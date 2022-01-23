using UnityEngine;

namespace Data.Scripts.BattleGrid
{
    public class GridBox : MonoBehaviour
    {
        [SerializeField] private Material standardMaterial;
        [SerializeField] private Material chosenMaterial;
        [SerializeField] private Material inRangeMaterial;

        private MeshRenderer _renderer;
    
        private void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public void RecolorToChosen()
        {
            _renderer.material = chosenMaterial;
        }

        public void RecolorToInRange()
        {
            _renderer.material = inRangeMaterial;
        }

        public void RecolorToStandard()
        {
            _renderer.material = standardMaterial;
        }
    
    }
}
