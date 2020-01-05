/*
 * Magic, Copyright(c) Thomas Hansen 2019 - 2020, thomas@servergardens.com, all rights reserved.
 * See the enclosed LICENSE file for details.
 */

using System.Linq;
using Newtonsoft.Json.Linq;
using magic.node;
using magic.node.extensions;
using magic.signals.contracts;

namespace magic.lambda.json.internals
{
    // TODO: Sanity check. Not entirely sure it actually works for all possible permutations.
    /// <summary>
    /// [.to-json-raw] slot for transforming to a raw Newtonsoft JSON JContainer object, without
    /// ever transforming to a string.
    /// </summary>
    [Slot(Name = ".lambda2json-raw")]
    public class Lambda2JsonRaw : ISlot
    {
        /// <summary>
        /// Slot implementation.
        /// </summary>
        /// <param name="signaler">Signaler that raised signal.</param>
        /// <param name="input">Arguments to slot.</param>
        public void Signal(ISignaler signaler, Node input)
        {
            if (input.Value != null)
                input.AddRange(input.Evaluate());

            var token = Handle(input);
            input.Clear();
            input.Value = token;
        }

        #region [ -- Private helper methods -- ]

        private JToken Handle(Node root)
        {
            JToken result = null;
            if (root.Children.Any(x => x.Name != "" && x.Name != "."))
            {
                // Complex object.
                var resObj = new JObject();
                foreach (var idx in root.Children)
                {
                    resObj.Add(HandleProperty(idx));
                }
                result = resObj;
            }
            else if (root.Children.Any())
            {
                // Array.
                var resArr = new JArray();
                foreach (var idx in root.Children)
                {
                    resArr.Add(HandleArray(idx));
                }
                result = resArr;
            }
            else
            {
                result = new JObject();
            }

            return result;
        }

        private JToken HandleArray(Node idx)
        {
            if (idx.Children.Any())
                return Handle(idx);

            return new JValue(idx.Value);
        }

        private JProperty HandleProperty(Node idx)
        {
            if (idx.Children.Any())
                return new JProperty(idx.Name, Handle(idx));

            return new JProperty(idx.Name, idx.Value);
        }

        #endregion
    }
}
