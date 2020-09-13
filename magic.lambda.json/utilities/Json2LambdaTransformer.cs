/*
 * Magic, Copyright(c) Thomas Hansen 2019 - 2020, thomas@servergardens.com, all rights reserved.
 * See the enclosed LICENSE file for details.
 */

using System;
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
            JToken2Node(node, container);
        }

        #region [ -- Private helper methods -- ]

        /*
         * Transforms a JToken to a node, and puts result into specified node.
         */
        static void JToken2Node(Node node, JToken token)
        {
            if (token is JArray array)
                JArray2Node(node, array);
            else if (token is JObject obj)
                JObject2Node(node, obj);
            else if (token is JValue val)
            {
                var value = val.Value;

                // Notice, we always assume everything we get in is UTC.
                if (value is DateTime dt)
                    value = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, DateTimeKind.Utc);
                node.Value = val.Value;
            }
        }

        /*
         * Transforms a JObject to a node, and puts result into specified node.
         */
        static void JObject2Node(Node node, JObject obj)
        {
            foreach (var idx in obj)
            {
                var cur = new Node(idx.Key);
                node.Add(cur);
                JToken2Node(cur, idx.Value);
            }
        }

        /*
         * Transforms a JArray to a node, and puts result into specified node.
         */
        static void JArray2Node(Node node, JArray arr)
        {
            foreach (var idx in arr)
            {
                var cur = new Node(".");
                node.Add(cur);
                JToken2Node(cur, idx);
            }
        }

        #endregion
    }
}
