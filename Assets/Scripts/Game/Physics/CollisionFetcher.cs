using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ContactInfo
{
    public ContactInfo(Vector2 newPos, Vector2 newNormal, Vector2 newRelVel, Vector2 newImpulse, Collider2D newCollider)
    {
        position = newPos;
        normal   = newNormal;
        relativeVelocity = newRelVel;
        impulse = newImpulse;
        other = newCollider;
    }
    public Vector2 position;
    public Vector2 normal;
    public Vector2 relativeVelocity;
    public Vector2 impulse;
    public Collider2D other;
}

public class CollisionFetcher : MonoBehaviour, IEnumerable<ContactInfo>
{
    private ContactInfo[,] contacts = new ContactInfo[2, 16];
    private int[] ContactCount = {0, 0};
    private int writeBuffer = 0;
    private int readBuffer = 1;
    private bool backwardsRead = false;

    public void FlipReadOrder()
    {
        backwardsRead = !backwardsRead;
    }

    private void RegisterCollision(Collision2D col)
    {
        for (int i = 0; i < col.contacts.Length; i++)
        {
            if (ContactCount[writeBuffer] < contacts.GetLength(1))
            {
                contacts[writeBuffer, ContactCount[writeBuffer]] = new ContactInfo(
                    col.contacts[i].point,
                    col.contacts[i].normal,
                    col.contacts[i].relativeVelocity,
                    new Vector2(col.contacts[i].tangentImpulse, col.contacts[i].normalImpulse),
                    col.contacts[i].otherCollider
                );
                ContactCount[writeBuffer]++;
            }
        }
    }

    public int GetContactCount()
    {
        return ContactCount[readBuffer];
    }

    private void FixedUpdate()
    {
        readBuffer = writeBuffer;
        writeBuffer = ( writeBuffer + 1 ) % 2;
        ContactCount[writeBuffer] = 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        RegisterCollision(col);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        RegisterCollision(col);
    }

    public IEnumerator<ContactInfo> GetEnumerator()
    {
        if (!backwardsRead)
        {
            for (int i = 0; i < ContactCount[readBuffer]; ++i)
            {
                yield return contacts[readBuffer, i];
            }
        }
        else
        {
            for (int i = ContactCount[readBuffer]-1;  i >= 0; --i)
            {
                yield return contacts[readBuffer, i];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
