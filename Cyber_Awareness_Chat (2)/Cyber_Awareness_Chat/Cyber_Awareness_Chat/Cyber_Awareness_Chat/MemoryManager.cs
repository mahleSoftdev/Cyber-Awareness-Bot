using System.Collections.Generic;

namespace Cyber_Awareness_Chat
{
    public class MemoryManager
    {
        private readonly Dictionary<string, string> memory;

        public MemoryManager()
        {
            memory = new Dictionary<string, string>();
        }

        // ─── STORE ────────────────────────────────────────────────
        public void StoreMemory(string key, string value)
        {
            if (memory.ContainsKey(key))
                memory[key] = value;
            else
                memory.Add(key, value);
        }

        // ─── RECALL ───────────────────────────────────────────────
        public string RecallMemory(string key)
        {
            return memory.ContainsKey(key) ? memory[key] : null;
        }

        // ─── CHECK IF KEY EXISTS ──────────────────────────────────
        public bool HasMemory(string key)
        {
            return memory.ContainsKey(key);
        }

        // ─── CLEAR ALL ────────────────────────────────────────────
        public void ClearMemory()
        {
            memory.Clear();
        }
    }
}