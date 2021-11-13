﻿/*
 * Magic Cloud, copyright Aista, Ltd. See the attached LICENSE file for details.
 */

using magic.node;
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
            input.Value = Lambda2JsonTransformer.ToJson(input);
            input.Clear();
        }
    }
}
