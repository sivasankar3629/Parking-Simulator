using Unity.VisualScripting;
using UnityEngine;

public class Trailer : MonoBehaviour
{
    [SerializeField] Transform _trailerAttachPoint;
    [SerializeField] Transform _truckAttachPoint;
    [SerializeField] Rigidbody _trailerRB;

    ConfigurableJoint _configurableJoint;

    private void Start()
    {
        _configurableJoint = GetComponent<ConfigurableJoint>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_configurableJoint != null)
            {
                Destroy(_configurableJoint);
                return;
            }

            _configurableJoint = gameObject.AddComponent<ConfigurableJoint>();
            _configurableJoint.autoConfigureConnectedAnchor = false;
            _configurableJoint.anchor = _truckAttachPoint.localPosition;

            _trailerRB.transform.rotation = _truckAttachPoint.transform.rotation;

            _configurableJoint.xMotion = ConfigurableJointMotion.Locked;
            _configurableJoint.yMotion = ConfigurableJointMotion.Locked;
            _configurableJoint.zMotion = ConfigurableJointMotion.Locked;

            _configurableJoint.angularXMotion = ConfigurableJointMotion.Limited;
            _configurableJoint.angularYMotion = ConfigurableJointMotion.Limited;
            _configurableJoint.angularZMotion = ConfigurableJointMotion.Limited;

            SoftJointLimit tempLimit = new SoftJointLimit();
            tempLimit.limit = -10f;
            _configurableJoint.lowAngularXLimit = tempLimit;
            tempLimit.limit = 10f;
            _configurableJoint.highAngularXLimit = tempLimit;

            tempLimit.limit = 35f;
            _configurableJoint.angularYLimit = tempLimit;
            tempLimit.limit = 5f;
            _configurableJoint.angularZLimit = tempLimit;

            _configurableJoint.connectedBody = _trailerRB;
            _configurableJoint.connectedAnchor = _trailerAttachPoint.localPosition;

        }
    }
}
