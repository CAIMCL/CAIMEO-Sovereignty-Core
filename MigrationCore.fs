namespace CAIMEO.Net.Sovereignty.Core

open System
open System.Collections.Generic

/// [Summary] Represents the status of Intellectual Property during the transition.
type MigrationStatus =
    | Legacy_RPI          // Origin: Research Prototype
    | InTransit           // Active migration to CAIMEO.NET
    | Sovereign           // Fully reclaimed and operational under CAIMEO.NET

/// [Summary] Represents a discrete unit of Clarion Intellectual Property.
type IPComponent =
    | CognitiveLogic      // The reasoning and reasoning engines
    | DistributedTables   // The core database schemas
    | NetworkTopology     // The distributed node architecture

/// [Summary] The primary record for reclaiming assets.
type ReclamationEntry = {
    Component: IPComponent
    LegacyID: string
    NewCAIMEO_ID: string option
    CurrentStatus: MigrationStatus
    SecurityLevel: int
}

module SovereigntyEngine =
    /// [Summary] Processes the migration of assets from RPI to CAIMEO.NET.
    /// This function serves as the technical declaration of sovereignty.
    let migrateAssets (inventory: ReclamationEntry list) =
        inventory |> List.map (fun entry ->
            match entry.CurrentStatus with
            | Legacy_RPI ->
                printfn "[SYSTEM] Initiating reclamation of %A (Legacy ID: %s)..." 
                    entry.Component entry.LegacyID
                { entry with 
                    CurrentStatus = InTransit; 
                    NewCAIMEO_ID = Some(Guid.NewGuid().ToString().Substring(0, 8)) }
            
            | InTransit ->
                printfn "[SYSTEM] Finalizing CAIMEO.NET Sovereign Host for %A." 
                    entry.Component
                { entry with CurrentStatus = Sovereign }
            
            | Sovereign ->
                printfn "[SYSTEM] %A is already Sovereign." entry.Component
                entry
        )

    /// [Summary] Displays the current state of the Sovereignty transition.
    let printMigrationReport (entries: ReclamationEntry list) =
        printfn "\n--- CAIMEO.NET SOVEREIGNTY REPORT ---"
        printfn "%-20s | %-15s | %-15s | %-15s" "Component" "Legacy ID" "CAIMEO ID" "Status"
        printfn "%-20s | %-15s | %-15s | %-15s" "-----" "----------" "----------" "-------"
        for e in entries do
            let caimeoId = e.NewCAIMEO_ID |> Option.or("N/A")
            printfn "%-20s | %-15s | %-15s | %-15s" 
                (sprintf "%A" e.Component) e.LegacyID caimeoId (sprintf "%A" e.CurrentStatus)
        printfn "----------------------------------------------------------------------\n"

// --- RECLAMATION INVENTORY ---
// This list represents the actual assets being moved away from RPI.
let clarionInventory = [
    { Component = CognitiveLogic; LegacyID = "CLARION_CORE_01"; NewCAIMEO_ID = None; CurrentStatus = Legacy_RPI; SecurityLevel = 5 }
    { Component = DistributedTables; LegacyID = "RPI_DATA_SET_A"; NewCAIMEO_ID = None; CurrentStatus = Legacy_RPI; SecurityLevel = 4 }
    { Component = NetworkTopology; LegacyID = "DIST_COGN_NET"; NewCAIMEO_ID = None; CurrentStatus = Legacy_RPI; SecurityLevel = 5 }
]

// --- EXECUTION ---
let finalState = SovereigntyEngine.migrateAssets clarionInventory
SovereigntyEngine.printMigrationReport finalState
