/*
 * Magic, Copyright(c) Thomas Hansen 2019 - 2021, thomas@servergardens.com, all rights reserved.
 * See the enclosed LICENSE file for details.
 */

using Newtonsoft.Json.Linq;
using magic.node;
using magic.node.extensions;
using magic.signals.contracts;
using magic.lambda.json.utilities;

namespace magic.lambda.json.internals
{
    /// <summary>
    /// [.from-json-raw] slot for transforming from a raw Newtonsoft JSON object to a lambda structure,
    /// without having to transform it to a string first.
    /// </summary>
    [Slot(Name = ".json2lambda-raw")]
    public class Json2LambdaRaw : ISlot
    {
        /// <summary>
        /// Slot implementation.
        /// </summary>
        /// <param name="signaler">Signaler that raised signal.</param>
        /// <param name="input">Arguments to slot.</param>
        public void Signal(ISignaler signaler, Node input)
        {
            input.Clear();
            Json2LambdaTransformer.ToNode(input, input.GetEx<JContainer>());
            input.Value = null;
        }
    }
}
