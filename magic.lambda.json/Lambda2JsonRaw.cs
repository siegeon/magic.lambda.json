/*
 * Magic, Copyright(c) Thomas Hansen 2019 - 2020, thomas@servergardens.com, all rights reserved.
 * See the enclosed LICENSE file for details.
 */

using System.Linq;
using magic.node;
using magic.node.extensions;
using magic.signals.contracts;
using magic.lambda.json.utilities;

namespace magic.lambda.json
{
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
            var tmp = new Node();
            if (input.Value != null)
                tmp.AddRange(input.Evaluate().Select(x => x.Clone()));
            else
                tmp.AddRange(input.Children.Select(x => x.Clone()));

            var jContainer = Lambda2JsonTransformer.ToJson(tmp);
            input.Clear();
            input.Value = jContainer;
        }
    }
}
