﻿namespace ExpenseTracker.Web.Services.Api;

public interface IApiClient
{
    Task<TResult> GetAsync<TResult>(string url);
    Task<TResult> PostAsync<TResult, TRequest>(string url, TRequest request);
    Task PutAsync<TRequest>(string url, TRequest request);
    Task DeleteAsync(string url);
}
