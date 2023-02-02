using System.Threading;
using Tasks;

// DONE :
// 1) One level of indentation per method
// 2) No Else (or ternary/switch with default)
// 4) First Class Collections
// 6) No Abbreviations
// 7) Small Entities
// 
// NOT DONE :
// 3) Wrap All Primitives and String
// 5) One Dot Per Line (Demeter Law)
// 8) Max 3 instance variables
// 9) No get/set/properties or public fields

new TaskList(new RealConsole())
    .Run(CancellationToken.None);