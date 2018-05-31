using UnityEngine;
using System.Collections.Generic;

namespace TrashHoarder {
	public class PlayerController : MonoBehaviour {
		[Header("Debug")]
		public bool useLocalCameraDirections = true;
		public int walkPathMaxLength = 100;
		public bool showPath = true;
		
		[Header("Settings")]
		public string player = "red";
		public int PlayerNumber = 1;
		public Color playerColor = Color.blue;
		public float movementSpeed = 5f;
		public float rotationSpeed = 5f;
		public float jumpForce = 500f;
		public int trashMinedToSpawnTrash = 5;
		public BigPile bigPile;
		public List<MineSmallPile> smallPiles = new List<MineSmallPile>();
		public GameObject[] garbage;
		public DropItems myPile;

		private Rigidbody rb;
		private Camera cam;
		private bool axisInUse = false;
		private bool canMine = true;
		private List<Vector3> walkPath = new List<Vector3>();
		private Transform pickupPoint;
		private Animator anim;

		private int mined = 0;
		private Transform pickedUpTrash;

		void Awake() {
			PlayerNumber = PlayerPrefs.GetInt(player.ToLower() + "Player", 0);
			if(PlayerNumber == 0) {
				Destroy(this);
				Destroy(GetComponent<Rigidbody>());
				Destroy(transform.GetChild(0).gameObject);
				Destroy(transform.GetChild(1).gameObject);
				myPile.size = -10;
			} else {
				transform.Find("Model").GetComponent<BoxCollider>().enabled = true;
			}
		}
		
		void Start() {
			rb = GetComponent<Rigidbody>();
			cam = GameObject.FindObjectOfType<Camera>();
			walkPath.Add(transform.position);
			pickupPoint = transform.Find("Pickup Spawnpoint");
			anim = transform.Find("Model").GetComponent<Animator>();
			axisInUse = false;
		}

		void canMineAgain() {
			canMine = true;
		}

		public void Mined() {
			mined++;
		}
		
		void Update() {
			if(mined >= trashMinedToSpawnTrash) {
				mined -= trashMinedToSpawnTrash;
				SpawnGarbage();
			}

			/*RaycastHit hit;
			if(Physics.Raycast(transform.position + (transform.up / 2f), transform.forward, out hit, 1.5f) && pickedUpTrash == null) {
				if(hit.transform.CompareTag("Trash")) {
					pickedUpTrash = hit.transform;
					hit.transform.tag = "TrashHolding";
					hit.transform.SetParent(pickupPoint);
					hit.transform.GetComponent<Rigidbody>().isKinematic = true;
					hit.transform.localEulerAngles = Vector3.zero;
					hit.transform.position = transform.position + (transform.up + transform.forward);
				}
			}*/

			Vector3 movePos = transform.position;
			if(useLocalCameraDirections) {
				if(Input.GetAxis("Vertical" + PlayerNumber) != 0f)
					movePos += cam.transform.parent.forward * ((Input.GetAxis("Vertical" + PlayerNumber) / 100f) * movementSpeed);

				if(Input.GetAxis("Horizontal" + PlayerNumber) != 0f)
					movePos += cam.transform.parent.right * ((Input.GetAxis("Horizontal" + PlayerNumber) / 100f) * movementSpeed);
			} else {
				if(Input.GetAxis("Vertical" + PlayerNumber) != 0f)
					movePos += transform.forward * ((Input.GetAxis("Vertical" + PlayerNumber) / 100f) * movementSpeed);

				if(Input.GetAxis("Horizontal" + PlayerNumber) != 0f)
					movePos += transform.right * ((Input.GetAxis("Horizontal" + PlayerNumber) / 100f) * movementSpeed);
			}

			if(bigPile != null) {
				if(Input.GetAxis("Fire" + PlayerNumber) == 1f && canMine) {
					bigPile.Mine(this);
					foreach(MineSmallPile p in smallPiles) {
						p.Mine(this);
					}
					canMine = false;
					//Invoke("canMineAgain", 0.25f);
				}
				if(Input.GetAxis("Fire" + PlayerNumber) == 0f && !canMine) {
					canMine = true;
				}
			}

			if(Vector3.Distance(transform.position, walkPath[walkPath.Count-1]) >= 1f) {
				walkPath.Add(transform.position);
				if(walkPath.Count > walkPathMaxLength)
					walkPath.RemoveAt(0);
			}

			if(Physics.Raycast(transform.position + transform.up, -transform.up, 1.001f)) {
				rb.isKinematic = true;
				if(Input.GetAxis("Jump" + PlayerNumber) != 0f && !axisInUse) {
					axisInUse = true;
					rb.isKinematic = false;
					rb.AddForce(Vector3.up * jumpForce);
				} else if(Input.GetAxis("Jump" + PlayerNumber) == 0f)
					axisInUse = false;
			}

			anim.SetBool("walking", false);
			if(transform.position != movePos) {
				rb.isKinematic = false;
				anim.SetBool("walking", true);
				rb.MovePosition(movePos);
			}

			Vector3 rotateDir = movePos - transform.position;
			float step = rotationSpeed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, rotateDir, step, 0f);
			transform.rotation = Quaternion.LookRotation(newDir);
			
		}

		public void SpawnGarbage() {
			GameObject trash = Instantiate(garbage[Random.Range(0, garbage.Length)], Vector3.zero, Quaternion.identity) as GameObject;
			trash.transform.position = pickupPoint.position;
		}

		void OnTriggerEnter(Collider hit) {
			if(hit.transform.CompareTag("Trash") && pickedUpTrash == null) {
				pickedUpTrash = hit.transform;
				hit.transform.tag = "TrashHolding";
				hit.transform.SetParent(pickupPoint);
				hit.transform.GetComponent<Rigidbody>().isKinematic = true;
				hit.transform.localEulerAngles = Vector3.zero;
				hit.transform.position = transform.position + (transform.up + transform.forward * 2f);
			}
		}

		void OnDrawGizmos() {
			if(!showPath)
				return;

			if(walkPath.Count <= 1)
				return;
			Gizmos.color = playerColor;
			for(int i = 1; i < walkPath.Count-1; i++) {
				Gizmos.DrawLine(walkPath[i-1], walkPath[i]);
			}
		}
	}
}
