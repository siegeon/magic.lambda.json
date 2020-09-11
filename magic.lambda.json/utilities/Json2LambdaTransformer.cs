/*
 * Magic, Copyright(c) Thomas Hansen 2019 - 2020, thomas@servergardens.com, all rights reserved.
 * See the enclosed LICENSE file for details.
 */

using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using magic.node;

namespace magic.lambda.json.utilities
{
    /*
     * Internal helper/implementation class, doing the heavy lifting.
     */
    internal static class Json2LambdaTransformer
    {
        /*
         * Transforms the given JContainer to a lambda object,
         * and injects into the given node.
         */
        internal static void ToNode(Node node, JContainer container)
        {
            node.Clear();
            node.Value = null;
            JToken2Node(node, container);
        }

        #region [ -- Private helper methods -- ]

        static void JToken2Node(Node node, JToken token)
        {
            if (token is JArray)
                JArray2Node(node, token as JArray);
            else if (token is JObject)
                JObject2Node(node, token as JObject);
            else if (token is JValue)
                node.Value = (token as JValue).Value;
        }

        static void JObject2Node(Node node, JObject obj)
        {
            foreach (var idx in obj)
            {
                var cur = new Node(idx.Key);
                node.Add(cur);
                JToken2Node(cur, idx.Value);
            }
        }

        static void JArray2Node(Node node, JArray arr)
        {
            foreach (var idx in arr)
            {
                // Special treatment for JObjects with only one property.
                if (idx is JObject)
                {
                    // Checking if object has only one property.
                    var obj = idx as JObject;
                    if (obj.Count == 1 && obj.First is JProperty jProp)
                    {
                        if (jProp.Value is JValue)
                        {
                            // Object is a simple object with a single value.
                            var prop = obj.First as JProperty;
                            node.Add(new Node(prop.Name, (prop.Value as JValue).Value));
                            continue;
                        }
                        else
                        {
                            // Object is a simple object with multiple properties.
                            var prop = obj.First as JProperty;
                            var tmp = new Node(prop.Name);
                            node.Add(tmp);
                            JToken2Node(tmp, prop.Value);
                            continue;
                        }
                    }
                }
                var cur = new Node();
                node.Add(cur);
                JToken2Node(cur, idx);
            }
        }

        #endregion
    }
}
