using System.Collections.Generic;
using UnityEngine;

public class BonesShuffler : MonoBehaviour
{
    [SerializeField] private DiceHolder _dicePfb;
    [SerializeField] private float _throwForce;
    [SerializeField] private ThrowZone _throwZone;
    [SerializeField] private int _simulationCount;
    [SerializeField] private float _offset;


    private List<int> FakedValues;
    private List<string> SideNames;
    private List<int> SideInts;
    private List<BoneCube> FakeCubes;
    private List<OriginalCube> OriginalCubes;
    private List<PrepareData> Datas;
    private int DiceCount;
    private Vector3 DirectForce;


    public void Build(int diceCount)
    {
        DiceCount = diceCount;  
        Physics.simulationMode = SimulationMode.FixedUpdate;
        DirectForce = Vector3.forward;
        FakeCubes = new List<BoneCube>();
        OriginalCubes = new List<OriginalCube>();
        Datas = new List<PrepareData>();
        SideInts = new List<int>();
        SideNames = new List<string>();
        _throwZone.ZoneMin.x -= diceCount*0.5f*_offset;
        _throwZone.ZoneMax.x -= diceCount*0.5f*_offset;

        for (int i = 0; i < DiceCount; i++)
        {
            SideInts.Add(0);
            SideNames.Add("");
            Datas.Add(new PrepareData());
            DiceHolder diceHolder = Instantiate(_dicePfb);
            FakeCubes.Add(diceHolder.FakeCube);
            OriginalCubes.Add(diceHolder.ViewCube);
            diceHolder.ViewCube.Configure();
        }
        
        for (int i = 0; i < DiceCount; i++)
        {
            for (int j = 0; j < DiceCount; j++)
            {
                Physics.IgnoreCollision(FakeCubes[i].Colider, OriginalCubes[j].Colider);
            }
        }
    }

    public void Shuffle(List<int> faledValues)
    {
        FakedValues = faledValues;

        PreShuffle();
        for (int i = 0; i < DiceCount; i++)
        {
            Throw(OriginalCubes[i].Rb, i);
            OriginalCubes[i].SetVisualValues(SideNames[i], FakedValues[i]);
            //OriginalCubes[i].SetVisualValues(SideInts[i], FakedValues[i]);
        }
    }

    private void PreShuffle()
    {
        Physics.simulationMode = SimulationMode.Script;
        for (int i = 0; i < DiceCount; i++)
        {
            RandomizeValues(i);
            Throw(FakeCubes[i].Rb, i);
        }

        //simulate
        for (int i = 0; i < _simulationCount; i++)
        {
            Physics.Simulate(Time.fixedDeltaTime);
        }

        for (int i = 0; i < DiceCount; i++)
        {
            SideInts[i] = FakeCubes[i].FindSideVector();
            SideNames[i] = FakeCubes[i].FindSideRay();
        }


        Physics.simulationMode = SimulationMode.FixedUpdate;
    }

    private void RandomizeValues(int i)
    {
        Datas[i] = new PrepareData();

        Datas[i].PrePosee = new
            (
                Random.Range(_throwZone.ZoneMin.x + i * _offset, _throwZone.ZoneMax.x + i * _offset),
                Random.Range(_throwZone.ZoneMin.y + i * _offset, _throwZone.ZoneMax.y + i * _offset),
                Random.Range(_throwZone.ZoneMin.z + i * _offset, _throwZone.ZoneMax.z + i * _offset)
            );

        Datas[i].PreRotate = new
            (
                Random.Range(0, 360),
                Random.Range(0, 360),
                Random.Range(0, 360)
            );
    }

    private void Throw(Rigidbody rb, int i)
    {
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.transform.position = Datas[i].PrePosee;
        rb.transform.rotation = Quaternion.Euler(Datas[i].PreRotate);
        rb.AddForce(DirectForce * _throwForce);
    }
}

[System.Serializable]
public class PrepareData
{
    public Vector3 PrePosee;
    public Vector3 PreRotate;
}

[System.Serializable]
public class ThrowZone
{
    public Vector3 ZoneMax;
    public Vector3 ZoneMin;
}

