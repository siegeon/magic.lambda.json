/*
 * Magic, Copyright(c) Thomas Hansen 2019, thomas@gaiasoul.com, all rights reserved.
 * See the enclosed LICENSE file for details.
 */

using System.Linq;
using Newtonsoft.Json.Linq;
using magic.node;
using magic.node.extensions;
using magic.signals.contracts;
using magic.lambda.json.utilities;

namespace magic.lambda.json
{
    /// <summary>
    /// [lambda2json] slot for transforming a lambda hierarchy to a JSON string.
    /// </summary>
    [Slot(Name = "lambda2json")]
    public class Lambda2Json : ISlot
    {
        /// <summary>
        /// Slot implementation.
        /// </summary>
        /// <param name="signaler">Signaler that raised signal.</param>
        /// <param name="input">Arguments to slot.</param>
        public void Signal(ISignaler signaler, Node input)
        {
            if (input.Value != null)
                input.AddRange(input.Evaluate().Select(x => x.Clone()));

            var token = Transformer.TransformToJSON(input);
            input.Clear();
            input.Value = token.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
