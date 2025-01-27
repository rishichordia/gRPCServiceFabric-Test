﻿using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System.Collections.Generic;
using System.Fabric;

namespace gRPCServer
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class gRPCServer : StatelessService
    {
        public gRPCServer(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            ServiceInstanceListener ser = new ServiceInstanceListener(initParams =>
                     new gRPCCommunicationListener(this.Context.ReplicaOrInstanceId.ToString()));
            var endpoint = FabricRuntime.GetActivationContext().GetEndpoint("ServiceEndpoint");
            ServiceEventSource.Current.ServiceMessage(this.Context, $"{endpoint.IpAddressOrFqdn}" + ":" + $"{endpoint.Port.ToString()}");
            
            return new[]
             { ser
            };
        }
 
    }
}
