﻿/*
 * Magic Cloud, copyright Aista, Ltd. See the attached LICENSE file for details.
 */

using System.Linq;
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
            var tmp = new Node();
            var format = false;
            if (input.Value != null)
            {
                format = input.Children.FirstOrDefault(x => x.Name == "format")?.GetEx<bool>() ?? false;
                tmp.AddRange(input.Evaluate().Select(x => x.Clone()));
            }
            else
            {
                tmp.AddRange(input.Children.Select(x => x.Clone()));
            }

            var jContainer = Lambda2JsonTransformer.ToJson(tmp);
            input.Clear();
            input.Value = jContainer.ToString(format ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None);
        }
    }
}
