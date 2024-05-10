using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject chainPart;
    public GameObject weaponPart;
    public int chainSize = 5;

    private void Start()
    {
        GenerateRope();
    }

    void GenerateRope()
    {
        Rigidbody2D prevBod = hook;
        for (int i = 0; i < chainSize; i++)
        {
            GameObject newSeg = Instantiate(chainPart);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;
            prevBod = newSeg.GetComponent<Rigidbody2D>();
            if (chainSize - 1 == i)
            {
                GameObject newWeapon = Instantiate(weaponPart);
                newWeapon.transform.parent = transform;
                newWeapon.transform.position = transform.position;
                HingeJoint2D hj2 = newWeapon.GetComponent<HingeJoint2D>();
                hj2.connectedBody = prevBod;
                prevBod = newWeapon.GetComponent<Rigidbody2D>();
            }
        }
    }
}
