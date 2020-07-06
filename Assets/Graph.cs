namespace X
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using UnityEngine;

    public class Graph : MonoBehaviour
    {
        private const float drag = 5f;

        private readonly IEnumerable<(string Subject, string Predicate, string Object)> triples = new[]
        {
            ("1", "qwer tyui op asdf ghjk l zxcv bnm", "1.1"),
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
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                var path = "eratosthenes.graphml";
                var graphml = File.OpenText(path).ReadToEnd();
                this.SendMessage(nameof(Process), graphml);
            }
        }

        public void Process(string graphml)
        {
            var document = XDocument.Parse(graphml);
            var @namespace = XNamespace.Get("http://graphml.graphdrawing.org/xmlns");
            var nodes = document.Descendants(
                @namespace.GetName("node")).Select(n => (
                    n.Attribute("id").Value,
                    n.Element(@namespace.GetName("data")).Value));
            var edges = document.Descendants(
                @namespace.GetName("edge")).Select(n => (
                    n.Attribute("source").Value,
                    n.Element(@namespace.GetName("data")).Value,
                    n.Attribute("target").Value));

            //var nodes = this.triples.Select(t => (t.Subject, t.Subject)).Union(this.triples.Select(t => (t.Object, t.Object))).Distinct();
            //var edges = this.triples;

            foreach (var node in nodes)
            {
                this.AddNode(node);
            }

            foreach (var triple in edges)
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
            edge.GetComponentInChildren<TextMesh>().text = CleanLabel(triple.Predicate);
            
            var edgeComponent = edge.GetComponent<Edge>();
            edgeComponent.Subject = subject;
            edgeComponent.Object = @object;
        }

        private void AddNode((string Id, string Label) data)
        {
            // parented to this graph
            var node = Instantiate(this.nodePrototype, this.transform);
            node.name = data.Id;
            node.GetComponentInChildren<TextMesh>().text = CleanLabel(data.Label);

            // so it settles down
            node.GetComponent<Rigidbody>().drag = drag;

            // bright and saturated colours
            node.GetComponent<Renderer>().material.color = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));

            // put them somewhere random
            // so layout works
            node.transform.position = new Vector3(Random.Range(0, 1000), Random.Range(0, 1000), Random.Range(0, 1000));
        }

        private static string CleanLabel(string text)
        {
            text = text.Replace("http://www.w3.org/1999/02/22-rdf-syntax-ns#", "rdf:");
            text = text.Replace("http://www.w3.org/2001/XMLSchema#", "xsd:");
            text = text.Replace("rdf:type", "a");
            text = text.Replace("http://example.com/", "ex:");

            return text;
        }
    }
}
