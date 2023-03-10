using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
   public GameObject Subject;
   public float ZPosition = 10f;

   private void FixedUpdate()
   {
      if (ReferenceEquals(Subject, null)) return;
      Transform subjectTransform = Subject.transform;

      transform.position= Vector3.Lerp(transform.position,
         new Vector3(subjectTransform.position.x, subjectTransform.position.y, -ZPosition), .45f);
   }
}
