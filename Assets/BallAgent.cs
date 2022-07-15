using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class BallAgent : Agent
{
    public GameObject ball;
    Rigidbody m_ballRb;
    EnvironmentParameters m_ResetParams;
    float xLimitMinPaddle = 1f;
    float xLimitMaxPaddle = 11f;
    float yLimitMinBall = -10f;
    float xLimitMaxBall = 8f;
    float xLimitMinBall = -2.5f;
    float zLimitBall = 1.77f;
    public void ResetBall()
    {
        Debug.Log("Resetting ball position");
        m_ballRb.position = new Vector3(0f, 1f, -3.5f);
    }
    void Update()
    {
        Vector3 pos = gameObject.transform.position;
        if (pos.x < xLimitMinPaddle) {
            pos.x = xLimitMinPaddle;
            gameObject.transform.position = pos;
        } 
        if (pos.x > xLimitMaxPaddle) {
            pos.x = xLimitMaxPaddle;            
            gameObject.transform.position = pos;
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(ball.transform.position.x);
    }
    public override void Initialize()
    {
        m_ballRb = ball.GetComponent<Rigidbody>();
        m_ResetParams = Academy.Instance.EnvironmentParameters;
        SetResetParameters();
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    { 
        float xMove = actionBuffers.ContinuousActions[0];
        gameObject.transform.position += new Vector3(Mathf.Clamp(xMove*10, -5.81f, 4.9f), 0, 0) * Time.deltaTime * 1f;
    }
    public override void Heuristic(in ActionBuffers actionsOut) {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");    
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ball") {
            AddReward(1f);
            Debug.Log("Hit Ball");
        } 
    }
    public void HitFloor() 
    {
        Debug.Log("End Episode");
        AddReward(-2f);
        EndEpisode();
    }
    public override void OnEpisodeBegin()
    {
        Debug.Log("New Episode");
        SetResetParameters();
    }
    public void SetPaddle()
    {
    }
    public void SetResetParameters()
    {
        SetPaddle();
    }
}
