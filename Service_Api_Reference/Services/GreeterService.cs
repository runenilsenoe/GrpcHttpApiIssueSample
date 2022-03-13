﻿using Greet;
using Grpc.Core;

namespace Service_Api_Reference.Services;

public class GreeterService : Greeter.GreeterBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply { Message = $"Hello {request.Name}" });
    }

    public override Task<HelloReply> SayHelloFrom(HelloRequestFrom request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply { Message = $"Hello {request.Name} from {request.From}" });
    }
}
