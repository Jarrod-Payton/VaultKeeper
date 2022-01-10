import { AppState } from "../AppState"

class ResetService {
  async ResetEverything() {
    AppState.activeKeep = {},
      AppState.activeUser = {},
      AppState.activeVault = {},
      AppState.keeps = [],
      AppState.vaults = []
  }

}

export const resetService = new ResetService()