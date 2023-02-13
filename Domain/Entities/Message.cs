using TransactionsExample.Domain.Interfaces;

namespace TransactionsExample.Domain.Entities;

public class Message : Entity
{
    public Message(string text, string email)
    {
        Text = text;
        Email = email;
    }

    public string Text { get; set; }
    public string Email { get; set; }
    public bool IsSent { get; set; }
    public bool Error { get; set; }

    public void Send()
    {
        if (string.IsNullOrEmpty(Email))
            throw new InvalidDataException("Email is required");

        if (IsSent)
            throw new InvalidOperationException("The message has already been sent");

        IsSent = true;
    }
}