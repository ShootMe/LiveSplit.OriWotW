using System;
using System.Diagnostics;
namespace LiveSplit.OriWotW {
    public enum PointerVersion {
        V1
    }
    public enum AutoDeref {
        None,
        Single,
        Double
    }
    public class ProgramSignature {
        public PointerVersion Version { get; set; }
        public string Signature { get; set; }
        public int Offset { get; set; }
        public ProgramSignature(PointerVersion version, string signature, int offset) {
            Version = version;
            Signature = signature;
            Offset = offset;
        }
        public override string ToString() {
            return Version.ToString() + " - " + Signature;
        }
    }
    public class ProgramPointer {
        private int lastID;
        private DateTime lastTry;
        private ProgramSignature[] signatures;
        private int[] offsets;
        public IntPtr Pointer { get; private set; }
        public PointerVersion Version { get; private set; }
        public AutoDeref AutoDeref { get; private set; }

        public ProgramPointer(AutoDeref autoDeref, params ProgramSignature[] signatures) {
            AutoDeref = autoDeref;
            this.signatures = signatures;
            lastID = -1;
            lastTry = DateTime.MinValue;
        }
        public ProgramPointer(AutoDeref autoDeref, params int[] offsets) {
            AutoDeref = autoDeref;
            this.offsets = offsets;
            lastID = -1;
            lastTry = DateTime.MinValue;
        }

        public T Read<T>(Process program, params int[] offsets) where T : struct {
            GetPointer(program);
            return program.Read<T>(Pointer, offsets);
        }
        public string Read(Process program, params int[] offsets) {
            GetPointer(program);
            return program.ReadString(Pointer, offsets);
        }
        public byte[] ReadBytes(Process program, int length, params int[] offsets) {
            GetPointer(program);
            return program.Read(Pointer, length, offsets);
        }
        public void Write<T>(Process program, T value, params int[] offsets) where T : struct {
            GetPointer(program);
            program.Write<T>(Pointer, value, offsets);
        }
        public void Write(Process program, byte[] value, params int[] offsets) {
            GetPointer(program);
            program.Write(Pointer, value, offsets);
        }
        public void ClearPointer() {
            Pointer = IntPtr.Zero;
        }
        public IntPtr GetPointer(Process program) {
            if (program == null) {
                Pointer = IntPtr.Zero;
                lastID = -1;
                return Pointer;
            } else if (program.Id != lastID) {
                Pointer = IntPtr.Zero;
                lastID = program.Id;
            }

            if (Pointer == IntPtr.Zero && DateTime.Now > lastTry) {
                lastTry = DateTime.Now.AddSeconds(1);

                Pointer = GetVersionedFunctionPointer(program);
                if (Pointer != IntPtr.Zero) {
                    if (AutoDeref != AutoDeref.None) {
                        if (MemoryReader.is64Bit) {
                            Pointer = (IntPtr)program.Read<ulong>(Pointer);
                        } else {
                            Pointer = (IntPtr)program.Read<uint>(Pointer);
                        }
                        if (AutoDeref == AutoDeref.Double) {
                            if (MemoryReader.is64Bit) {
                                Pointer = (IntPtr)program.Read<ulong>(Pointer);
                            } else {
                                Pointer = (IntPtr)program.Read<uint>(Pointer);
                            }
                        }
                    }
                }
            }
            return Pointer;
        }
        private IntPtr GetVersionedFunctionPointer(Process program) {
            if (signatures != null) {
                MemorySearcher searcher = new MemorySearcher();
                ulong gameAsmStart = 0, gameAsmEnd = 0;
                for (int i = 0; i < program.Modules.Count; i++) {
                    ProcessModule module = program.Modules[i];
                    if (module.ModuleName.Equals("GameAssembly.dll", StringComparison.OrdinalIgnoreCase)) {
                        gameAsmStart = (ulong)module.BaseAddress;
                        gameAsmEnd = gameAsmStart + (ulong)module.ModuleMemorySize;
                        break;
                    }
                }

                searcher.MemoryFilter = delegate (MemInfo info) {
                    return (ulong)info.BaseAddress >= gameAsmStart && (ulong)info.BaseAddress <= gameAsmEnd && (info.State & 0x1000) != 0 && (info.Protect & 0x20) != 0 && (info.Protect & 0x100) == 0;
                };
                for (int i = 0; i < signatures.Length; i++) {
                    ProgramSignature signature = signatures[i];

                    IntPtr ptr = searcher.FindSignature(program, signature.Signature);
                    if (ptr != IntPtr.Zero) {
                        Version = signature.Version;
                        int offset = 0;
                        if (AutoDeref != AutoDeref.None) {
                            offset = program.Read<int>(ptr + signature.Offset) + 4;
                        }
                        return ptr + signature.Offset + offset;
                    }
                }
                return IntPtr.Zero;
            }

            if (MemoryReader.is64Bit) {
                return (IntPtr)program.Read<ulong>(program.MainModule.BaseAddress, offsets);
            } else {
                return (IntPtr)program.Read<uint>(program.MainModule.BaseAddress, offsets);
            }
        }
    }
}