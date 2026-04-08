# Cancel-Subscription Example

```csharp
var client = new SharpIppClient();
var request = new CancelSubscriptionRequest();
var response = await client.CancelSubscriptionAsync(request);
```
