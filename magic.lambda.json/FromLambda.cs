/*
 * Magic, Copyright(c) Thomas Hansen 2019 - thomas@gaiasoul.com
 * Licensed as Affero GPL unless an explicitly proprietary license has been obtained.
 */

using System.Linq;
using Newtonsoft.Json.Linq;
using magic.node;
using magic.node.extensions;
using magic.signals.contracts;
using magic.lambda.json.utilities;

namespace magic.lambda.json
{
    // TODO: Sanity check. Not entirely sure it actually works for all possible permutations.
    /// <summary>
    /// [json.from-lambda] slot for transforming a lambda hierarchy to a JSON string.
    /// </summary>
    [Slot(Name = "json.from-lambda")]
    [Slot(Name = "json.to-json")]
    public class FromLambda : ISlot
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
