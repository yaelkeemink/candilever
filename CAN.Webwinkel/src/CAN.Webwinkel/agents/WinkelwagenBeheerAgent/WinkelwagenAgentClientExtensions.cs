// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace CAN.Webwinkel.Agents.WinkelwagenAgent
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;
    using Models;

    /// <summary>
    /// Extension methods for WinkelwagenAgentClient.
    /// </summary>
    public static partial class WinkelwagenAgentClientExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='winkelmandje'>
            /// </param>
            public static object Update(this IWinkelwagenAgentClient operations, Winkelmandje winkelmandje = default(Winkelmandje))
            {
                return Task.Factory.StartNew(s => ((IWinkelwagenAgentClient)s).UpdateAsync(winkelmandje), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='winkelmandje'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> UpdateAsync(this IWinkelwagenAgentClient operations, Winkelmandje winkelmandje = default(Winkelmandje), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.UpdateWithHttpMessagesAsync(winkelmandje, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='winkelmandje'>
            /// </param>
            public static object Post(this IWinkelwagenAgentClient operations, Winkelmandje winkelmandje = default(Winkelmandje))
            {
                return Task.Factory.StartNew(s => ((IWinkelwagenAgentClient)s).PostAsync(winkelmandje), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='winkelmandje'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> PostAsync(this IWinkelwagenAgentClient operations, Winkelmandje winkelmandje = default(Winkelmandje), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.PostWithHttpMessagesAsync(winkelmandje, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bestelling'>
            /// </param>
            public static ErrorMessage Afronden(this IWinkelwagenAgentClient operations, Bestelling bestelling = default(Bestelling))
            {
                return Task.Factory.StartNew(s => ((IWinkelwagenAgentClient)s).AfrondenAsync(bestelling), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bestelling'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ErrorMessage> AfrondenAsync(this IWinkelwagenAgentClient operations, Bestelling bestelling = default(Bestelling), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.AfrondenWithHttpMessagesAsync(bestelling, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
