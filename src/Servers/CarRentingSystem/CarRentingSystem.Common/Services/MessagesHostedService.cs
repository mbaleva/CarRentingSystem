﻿using CarRentingSystem.Common.Data.Models;
using Hangfire;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentingSystem.Common.Services;

public class MessagesHostedService : IHostedService
{
    private readonly IRecurringJobManager recurringJob;
    private readonly DbContext data;
    private readonly IBus publisher;

    public MessagesHostedService(
        IRecurringJobManager recurringJob,
        DbContext data,
        IBus publisher)
    {
        this.recurringJob = recurringJob;
        this.data = data;
        this.publisher = publisher;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        this.recurringJob.AddOrUpdate(
            nameof(MessagesHostedService),
            () => this.ProcessPendingMessages(),
            "*/10 * * * * *");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;

    public void ProcessPendingMessages()
    {
        var messages = this.data
            .Set<Message>()
            .Where(m => !m.Published)
            .OrderBy(m => m.Id)
            .ToList();

        foreach (var message in messages)
        {
            this.publisher
                .Publish(message.Data, message.Type)
                .GetAwaiter()
                .GetResult();

            message.Published = true;

            this.data.SaveChanges();
        }
    }
}
