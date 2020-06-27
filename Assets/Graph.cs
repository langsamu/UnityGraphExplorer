namespace X
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class Graph : MonoBehaviour
    {
        private const float drag = 10f;

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
            ("1.1.1", "", "1.1.1.1"),
            ("1.1.1", "", "1.1.1.2"),
            ("1.1.1", "", "1.1.1.3"),
            ("1.1.1", "", "1.1.1.4"),
            ("1.1.2", "", "1.1.2.1"),
            ("1.1.2", "", "1.1.2.2"),
            ("1.1.2", "", "1.1.2.3"),
            ("1.1.2", "", "1.1.2.4"),
            ("1.1.3", "", "1.1.3.1"),
            ("1.1.3", "", "1.1.3.2"),
            ("1.1.3", "", "1.1.3.3"),
            ("1.1.3", "", "1.1.3.4"),
            ("1.1.4", "", "1.1.4.1"),
            ("1.1.4", "", "1.1.4.2"),
            ("1.1.4", "", "1.1.4.3"),
            ("1.1.4", "", "1.1.4.4"),
            ("1.2.1", "", "1.2.1.1"),
            ("1.2.1", "", "1.2.1.2"),
            ("1.2.1", "", "1.2.1.3"),
            ("1.2.1", "", "1.2.1.4"),
            ("1.2.2", "", "1.2.2.1"),
            ("1.2.2", "", "1.2.2.2"),
            ("1.2.2", "", "1.2.2.3"),
            ("1.2.2", "", "1.2.2.4"),
            ("1.2.3", "", "1.2.3.1"),
            ("1.2.3", "", "1.2.3.2"),
            ("1.2.3", "", "1.2.3.3"),
            ("1.2.3", "", "1.2.3.4"),
            ("1.2.4", "", "1.2.4.1"),
            ("1.2.4", "", "1.2.4.2"),
            ("1.2.4", "", "1.2.4.3"),
            ("1.2.4", "", "1.2.4.4"),
            ("1.3.1", "", "1.3.1.1"),
            ("1.3.1", "", "1.3.1.2"),
            ("1.3.1", "", "1.3.1.3"),
            ("1.3.1", "", "1.3.1.4"),
            ("1.3.2", "", "1.3.2.1"),
            ("1.3.2", "", "1.3.2.2"),
            ("1.3.2", "", "1.3.2.3"),
            ("1.3.2", "", "1.3.2.4"),
            ("1.3.3", "", "1.3.3.1"),
            ("1.3.3", "", "1.3.3.2"),
            ("1.3.3", "", "1.3.3.3"),
            ("1.3.3", "", "1.3.3.4"),
            ("1.3.4", "", "1.3.4.1"),
            ("1.3.4", "", "1.3.4.2"),
            ("1.3.4", "", "1.3.4.3"),
            ("1.3.4", "", "1.3.4.4"),
            ("1.4.1", "", "1.4.1.1"),
            ("1.4.1", "", "1.4.1.2"),
            ("1.4.1", "", "1.4.1.3"),
            ("1.4.1", "", "1.4.1.4"),
            ("1.4.2", "", "1.4.2.1"),
            ("1.4.2", "", "1.4.2.2"),
            ("1.4.2", "", "1.4.2.3"),
            ("1.4.2", "", "1.4.2.4"),
            ("1.4.3", "", "1.4.3.1"),
            ("1.4.3", "", "1.4.3.2"),
            ("1.4.3", "", "1.4.3.3"),
            ("1.4.3", "", "1.4.3.4"),
            ("1.4.4", "", "1.4.4.1"),
            ("1.4.4", "", "1.4.4.2"),
            ("1.4.4", "", "1.4.4.3"),
            ("1.4.4", "", "1.4.4.4"),
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
            var subject = GameObject.Find(triple.Subject);
            var @object = GameObject.Find(triple.Object);

            // parented to subject node
            var edge = Instantiate(this.edgePrototype, subject.transform.transform);
            edge.name = triple.Subject + "-" + triple.Object;

            var edgeComponent = edge.GetComponent<Edge>();
            edgeComponent.Subject = subject;
            edgeComponent.Object = @object;
        }

        private void AddNode(string name)
        {
            // parented to this graph
            var node = Instantiate(this.nodePrototype, this.transform);
            node.name = name;

            // so it settles down
            node.GetComponent<Rigidbody>().drag = drag;

            var r = Random.Range(0f, 1f);

            // bright and saturated colours
            node.GetComponent<Renderer>().material.color = Color.HSVToRGB(r, Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));

            node.GetComponentInChildren<AudioSource>().pitch = r * 2f;

            // put them somewhere random
            // so layout works
            node.transform.position = new Vector3(Random.Range(0, 1000), Random.Range(0, 1000), Random.Range(0, 1000));
        }
    }
}
