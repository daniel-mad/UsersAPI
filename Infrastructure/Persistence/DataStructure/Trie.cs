using CsvHelper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.DataStructure;
public class Trie
{
    private class Node
    {
        public Dictionary<Guid, User> data = new();
        public Dictionary<char, Node> children = new();
        public bool isEndOfWord = false;

    }

    private Node root = new Node();
    public void Insert(string word, User user)
    {
        var current = root;
        foreach (var ch in word)
        {
            if (!current.children.ContainsKey(ch))
            {
                current.children[ch] = new();
            }
            current.children[ch].data.TryAdd(user.Id, user);
            current = current.children[ch];
        }
        current.isEndOfWord = true;
    }

    public List<User> StartWith(string word)
    {
        var current = root;
        foreach (var ch in word)
        {
            if (!current.children.ContainsKey(ch))
                return null;
            current = current.children[ch];
        }
        var users = current.data.Values.ToList();
        return users;
    }

    public void Remove(string word, Guid id)
    {
        var current = root;
        foreach (var ch in word)
        {
            if (current.children.ContainsKey(ch))
            {
                current.children[ch].data.Remove(id);
            }
            current = current.children[ch];
        }
    }
}
