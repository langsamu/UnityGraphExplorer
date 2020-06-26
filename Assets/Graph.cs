namespace X
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class Graph : MonoBehaviour
    {
        private readonly IEnumerable<(string Subject, string Predicate, string Object)> triples = new[]
        {
            ("1", "", "1.1"),
            ("1", "", "1.2"),
            ("1", "", "1.3"),
            ("1", "", "1.4"),
            ("1.1", "", "1.1.1"),
            ("1.1", "", "1.1.2"),
            ("1.1", "", "1.1.3"),
            ("1.1", "", "1.1.4"),
            ("1.2", "", "1.2.1"),
            ("1.2", "", "1.2.2"),
            ("1.2", "", "1.2.3"),
            ("1.2", "", "1.2.4"),
            ("1.3", "", "1.3.1"),
            ("1.3", "", "1.3.2"),
            ("1.3", "", "1.3.3"),
            ("1.3", "", "1.3.4"),
            ("1.4", "", "1.4.1"),
            ("1.4", "", "1.4.2"),
            ("1.4", "", "1.4.3"),
            ("1.4", "", "1.4.4"),
        };

        public GameObject nodePrototype;
        public GameObject edgePrototype;

        public void Start()
        {
            var nodes = this.triples.Select(t => t.Subject).Union(this.triples.Select(t => t.Object)).Distinct();

            foreach (var node in nodes)
            {
                this.AddNode(node);
            }

            foreach (var triple in this.triples)
            {
                this.AddEdge(triple);
            }
        }

        private void AddEdge((string Subject, string Predicate, string Object) triple)
        {
            var edgeGO = Instantiate(this.edgePrototype);
            edgeGO.name = triple.Subject + "-" + triple.Object;

            var edgeComponent = edgeGO.GetComponent<Edge>();
            edgeComponent.Subject = GameObject.Find(triple.Subject);
            edgeComponent.Object = GameObject.Find(triple.Object);
        }

        private void AddNode(string node)
        {
            var nodeinstance = Instantiate(this.nodePrototype);
            nodeinstance.name = node;

            // put them somewhere random
            // so layout works
            nodeinstance.transform.position = new Vector3(Random.Range(0, 1000), Random.Range(0, 1000), Random.Range(0, 1000));
        }
    }
}
