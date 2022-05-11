using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Utilities
{
    public class ObjectImporter : AssetPostprocessor
    {
        public override int GetPostprocessOrder()
        {
            return 100;
        }

        public void OnPreprocessMaterialDescription(MaterialDescription description, Material material,
            AnimationClip[] materialAnimation)
        {
            List<string> props = new List<string>();
            description.GetTexturePropertyNames(props);
            
            var shader = Shader.Find("Shader Graphs/PBR");
            material.shader = shader;
            
            material.SetFloat("_WorkflowMode", GetShaderType(description));
            
            TrySetTexture(material, description, "DiffuseColor", "_Diff");
            TrySetTexture(material, description, "ShininessExponent", "_Roughness", 
                "_HasRoughness");
            TrySetTexture(material, description, "SpecularFactor", "_Specular", 
                "_HasSpecular");
            TrySetTexture(material, description, "ReflectionFactor", "_Metal", 
                "_HasMetal");
            TrySetTexture(material, description, "NormalMap", "_Normal");
        }

        private void TrySetTexture(Material material, MaterialDescription description, string propertyName,
            string shaderPropertyName)
        {
            if (description.TryGetProperty(propertyName, out TexturePropertyDescription textureProperty))
            {
                material.SetTexture(shaderPropertyName, textureProperty.texture);
            }
        }
        
        private void TrySetTexture(Material material, MaterialDescription description, string propertyName,
            string shaderPropertyName, string boolName)
        {
            if (description.TryGetProperty(propertyName, out TexturePropertyDescription textureProperty))
            {
                material.SetTexture(shaderPropertyName, textureProperty.texture);
                material.SetFloat(boolName, 1f);
            }
        }

        private float GetShaderType(MaterialDescription description)
        {
            return description.TryGetProperty("ReflectionFactor", out TexturePropertyDescription textureProperty)
                ? 1f
                : 0f;
        }
    }
}
