using UnityEngine;

public class DiceHolder : MonoBehaviour
{
    [SerializeField] private BoneCube _fakeCube;
    [SerializeField] private OriginalCube _viewCube;

    public BoneCube FakeCube => _fakeCube;
    public OriginalCube ViewCube => _viewCube;

}
