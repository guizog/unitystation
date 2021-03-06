﻿using UnityEngine;
using System.Collections;

namespace PlayGroup
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicsMove: MonoBehaviour
    {

        private Rigidbody2D thisRigi;
        private PlayerScript parentScript;

        public bool isMoving = false;
        public bool lerpA = false;
        public bool isSpaced = false;
        public bool triedToMoveInSpace = false;
        public bool keyDown = false;

        private Vector2 moveDirection;

        public Vector2 MoveDirection { get { return moveDirection; } }

        private Vector3 startPos;
        private Vector3 node;
        private Vector3 toClamp;

        public float lerpSpeed = 10f;
        private float lerpTime = 0f;
        public float clampPos;
        public float moveSpeed;

        void Start()
        {
            parentScript = GetComponent<PlayerScript>();
            thisRigi = GetComponent<Rigidbody2D>();
            GameObject findGM = GameObject.FindGameObjectWithTag("GameManager");
        }

        void Update()
        {
            //when movekeys are released then take the character to the nearest node
            if (lerpA)
            {
                LerpToTarget();
            }
        }

        void FixedUpdate()
        {
            if (isMoving)
            {
                //Move character via RigidBody
                MoveRigidBody();
            }
        }

        //move in direction input
        public void MoveInDirection(Vector2 direction)
        { 
            lerpA = false;
            moveDirection = direction;
            isMoving = true;

            if (direction == Vector2.right || direction == Vector2.left)
            {
                clampPos = transform.position.y; 
            }

            if (direction == Vector2.down || direction == Vector2.up)
            {
                clampPos = transform.position.x;
            }
        }

        //force a snap to the tile manually
        public void ForceSnapToTile()
        {
            startPos = transform.position;
            moveDirection = Vector2.zero;
            node = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0f);
            if (thisRigi != null)
                thisRigi.velocity = Vector3.zero;

            lerpTime = 0f;
            lerpA = true;
        }

        //movement input stopped, only use this to snap if there is an applied velocity
        public void MoveInputReleased()
        {
            startPos = transform.position;
            node =  Matrix.Matrix.GetClosestNode(transform.position, thisRigi.velocity);

            //if target !isPassable then snap back to currentTile
            if (node.x == Mathf.Round(startPos.x) && node.y == Mathf.Round(startPos.y))
            {
                thisRigi.MovePosition(node);
                isMoving = false;
                thisRigi.velocity = Vector3.zero;
            }
            else
            {
                //else lerp to the target tile depending on direction travelling
                thisRigi.velocity = Vector3.zero;
                lerpTime = 0f;
                lerpA = true;
            }
        }

        //Most used for Shuffling Players out of tiles when being occupied by others
        [PunRPC]
        public void ManualMoveToTarget(Vector3 targetPos)
        {
            startPos = transform.position;
            node = targetPos;
            thisRigi.velocity = Vector3.zero;
            lerpTime = 0f;
            lerpA = true;
        }

        //used with LerpA in update
        private void LerpToTarget()
        {
            lerpTime += Time.deltaTime;
            float t = lerpTime * lerpSpeed;

            thisRigi.MovePosition(Vector2.Lerp(startPos, node, t));

            if (moveDirection == Vector2.right && transform.position.x >= node.x)
            {
                isMoving = false;
                lerpA = false;
                thisRigi.velocity = Vector3.zero;
            }
            if (moveDirection == Vector2.left && transform.position.x <= node.x)
            {
                isMoving = false;
                lerpA = false;
                thisRigi.velocity = Vector3.zero;
            }
            if (moveDirection == Vector2.up && transform.position.y >= node.y)
            {
                isMoving = false;
                lerpA = false;
                thisRigi.velocity = Vector3.zero;
            }
            if (moveDirection == Vector2.down && transform.position.y <= node.y)
            {
                isMoving = false;
                lerpA = false;
                thisRigi.velocity = Vector3.zero;
            }

            if (moveDirection == Vector2.zero && transform.position == node)
            {
                isMoving = false;
                lerpA = false;
                thisRigi.velocity = Vector3.zero;
            }
        }

        //move the character via RigidBody in FixedUpdate
        private void MoveRigidBody()
        {
            if (!triedToMoveInSpace)
            {
                if (moveDirection == Vector2.down)
                {
                    thisRigi.velocity = new Vector3(0f, moveDirection.y, 0).normalized * moveSpeed;
                    toClamp = transform.position;
                    Mathf.Clamp(toClamp.x, clampPos, clampPos);
                    transform.position = toClamp;
                    return;
                }
                if (moveDirection == Vector2.up)
                {
                    thisRigi.velocity = new Vector3(0f, moveDirection.y, 0).normalized * moveSpeed;
                    toClamp = transform.position;
                    Mathf.Clamp(toClamp.x, clampPos, clampPos);
                    transform.position = toClamp;
                    return;
                }
                if (moveDirection == Vector2.right)
                {
                    thisRigi.velocity = new Vector3(moveDirection.x, 0f, 0).normalized * moveSpeed;
                    toClamp = transform.position;
                    Mathf.Clamp(toClamp.y, clampPos, clampPos);
                    transform.position = toClamp;
                    return;
                }
                if (moveDirection == Vector2.left)
                {
                    thisRigi.velocity = new Vector3(moveDirection.x, 0f, 0).normalized * moveSpeed;
                    toClamp = transform.position;
                    Mathf.Clamp(toClamp.y, clampPos, clampPos);
                    transform.position = toClamp;
                    return;
                }
            }

            if (isSpaced && !triedToMoveInSpace)
            {
                triedToMoveInSpace = true;
                thisRigi.mass = 0f;
                thisRigi.drag = 0f;
                thisRigi.angularDrag = 0f; 
            }
        }
            
    }
}
