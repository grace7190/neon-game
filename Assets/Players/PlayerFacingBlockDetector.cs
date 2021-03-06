﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFacingBlockDetector : MonoBehaviour {

    public GameObject HighlightedObject = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Block && (other.gameObject.layer == Layers.Solid | other.gameObject.layer == Layers.FloorBlocks))
        {
            var block = other.GetComponent<Block> ();

            if (gameObject.name == "Player1Child")
            {
                block.ChangeColor (Color.blue, Block.ChangeColorDuration);
            }
            else
            {
                block.ChangeColor (Color.green, Block.ChangeColorDuration);
            }
            HighlightedObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.Block) {
            var block = other.GetComponent<Block> ();
            block.ChangeColor (block.BaseColor, Block.ChangeColorDuration);

            if (HighlightedObject == other.gameObject)
            {
                HighlightedObject = null;
            }
        }
    }

    void OnDestroy()
    {
        Reset();
    }

    public void Reset()
    {
        if (HighlightedObject != null)
        {
            var block = HighlightedObject.GetComponent<Block>();
            block.ChangeColor (block.BaseColor, Block.ChangeColorDuration);
        }
    }
}
