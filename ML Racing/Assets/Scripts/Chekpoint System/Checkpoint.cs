using UnityEngine;

public class Checkpoint : MonoBehaviour
{ 
  private void OnTriggerEnter(Collider other)
  {
      if(other.TryGetComponent<CarController>(out CarController carController))
      {
          //this.gameObject.SetActive(false);
          this.gameObject.GetComponentInParent<TrackCheckPoints>().SetColObject(other.gameObject, this.gameObject);
      }
  }
}
