using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit.Utilities;

namespace UnityEngine.XR.Interaction.Toolkit
{
    public class XRPokeInteractor : XRSocketInteractor
    {
        public override bool isSelectActive => false;

        public override bool CanSelect(XRBaseInteractable interactable) {
            return false;
        }

        protected override bool ShouldDrawHoverMesh(MeshFilter meshFilter, Renderer meshRenderer, Camera mainCamera) {
            return false;
        }
    }
}