import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {

  async setupVaultPage(vaultId) {
    const vault = await this.getVaultById(vaultId)
    const keeps = await this.getKeepsByVaultId(vaultId)
    AppState.activeVault = vault
    AppState.keeps = keeps
  }

  async getVaultById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`)
    logger.log('Vault', res.data)
    return res.data
  }
  async getKeepsByVaultId(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log('Keeps', res.data)
    return res.data
  }
  async GetMyVaults() {
    const res = api.get(`account/vaults`)
    logger.log('My Vaults', res.data)
    AppState.myVaults = res.data
  }
}

export const vaultsService = new VaultsService()