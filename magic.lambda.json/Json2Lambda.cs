/*
 * Magic, Copyright(c) Thomas Hansen 2019 - 2020, thomas@servergardens.com, all rights reserved.
 * See the enclosed LICENSE file for details.
 */

using Newtonsoft.Json.Linq;
using magic.node;
using magic.node.extensions;
using magic.signals.contracts;
using magic.lambda.json.utilities;

namespace magic.lambda.json
{
    /// <summary>
    /// [json2lambda] slot for transforming a piece of JSON to a lambda hierarchy.
    /// </summary>
    [Slot(Name = "json2lambda")]
    public class Json2Lambda : ISlot
    {
        /// <summary>
        /// Slot implementation.
        /// </summary>
        /// <param name="signaler">Signaler that raised signal.</param>
        /// <param name="input">Arguments to slot.</param>
        public void Signal(ISignaler signaler, Node input)
        {
            input.Clear();
            Json2LambdaTransformer.ToNode(input, JContainer.Parse(input.GetEx<string>()) as JContainer);
            input.Value = null;
        }
    }
}
