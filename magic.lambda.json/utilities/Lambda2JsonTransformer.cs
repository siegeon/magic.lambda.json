/*
 * Magic, Copyright(c) Thomas Hansen 2019 - 2020, thomas@servergardens.com, all rights reserved.
 * See the enclosed LICENSE file for details.
 */

using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using magic.node;
using magic.node.extensions;

namespace magic.lambda.json.utilities
{
    /*
     * Internal helper/implementation class, doing the heavy lifting.
     */
    internal static class Lambda2JsonTransformer
    {
        /*
         * Creates a JContainer from the specified lambda/node object.
         */
        internal static JContainer ToJson(Node node)
        {
            return Node2JContainer(node);
        }

        #region [ -- Private helper methods -- ]

        /*
         * Recursively transforms the given node to a JContainer,
         * and returns the results to caller.
         */
        static JContainer Node2JContainer(Node root)
        {
            if (root.Children.Any(x => x.Name.Length > 0 && x.Name != "."))
                return new JObject(root.Children.Select(x => Node2JProperty(x))); // JObject with properties
            else if (root.Children.Any())
                return new JArray(root.Children.Select(x => Node2JToken(x))); // Array
            return new JObject();
        }

        /*
         * Recursively transforms the given node to a JProperty,
         * and returns the results to caller.
         */
        static JProperty Node2JProperty(Node idx)
        {
            if (idx.Children.Any())
                return new JProperty(idx.Name, Node2JContainer(idx));
            return new JProperty(idx.Name, Node2JValue(idx));
        }

        /*
         * Recursively transforms the given node to a JToken,
         * and returns the results to caller.
         */
        static JToken Node2JToken(Node idx)
        {
            if (idx.Children.Any())
                return Node2JContainer(idx);
            else
                return Node2JValue(idx);
        }

        /*
         * Transforms the given node to a JValue,
         * and returns the results to caller.
         */
        static JValue Node2JValue(Node idx)
        {
            // Notice, for JSON we want to return dates as UTC, JavaScript style!
            var value = idx.GetEx<object>();

            // Notice, we always return everything as UTC.
            if (value is DateTime dateValue)
                value = dateValue.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            return new JValue(value);
        }

        #endregion
    }
}
