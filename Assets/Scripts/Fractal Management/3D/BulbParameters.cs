using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Linq;

[CreateAssetMenu(fileName = "Bulb params ", menuName = "Bulb params")]
public class BulbParameters : ScriptableObject, IEnumerable<(string, float)>
{
    [Range(0, Mathf.PI)] [SerializeField] float Offset = Mathf.PI / 2;
    
    [Range(10, 30)] [SerializeField] float Halo = 25;
    
    [Range(0, 0.15f)] [SerializeField] float BgBlending = 0.04f;
    
    [Range(0, 1)] [SerializeField] float ColorVariation = 0.64f;
    
    [Range(0, 2 * Mathf.PI)] [SerializeField] float HueOffset = 3.3f;
    
    [Range(0, 10)] [SerializeField] float HueRange = 3.5f;
    
    [Range(0, 1)] [SerializeField] float DarkCorrection = 0.55f;
    
    [Range(0, 1)] [SerializeField] float Contrast = 0.5f;
    
    [Range(-1, 1)] [SerializeField] float Saturation = 0.3f;
    
    [Range(0, 1)] [SerializeField] float OuterVignette = 0.6f;
    
    [Range(0, 1)] [SerializeField] float InnerVignette = 0.64f;

    [Range(0, 1)] [SerializeField] float Test = 0;

    public IEnumerator<(string, float)> GetEnumerator() => GetType()
        .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(field => field.FieldType == typeof(float))
        .Select(field => (field.Name, (float)field.GetValue(this)))
        .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}