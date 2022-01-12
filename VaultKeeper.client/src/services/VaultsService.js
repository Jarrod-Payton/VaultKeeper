import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
import { keepsService } from "./KeepsService"

class VaultsService {

  async setupVaultPage(vaultId) {
    const vault = await this.getVaultById(vaultId)
    const keeps = await this.getKeepsByVaultId(vaultId)
    AppState.activeVault = vault
    AppState.keeps = keeps
    this.sort('mostRecent')
    keepsService.sort('mostRecent')
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
    const res = await api.get(`account/vaults`)
    logger.log('My Vaults', res.data)
    AppState.myVaults = res.data
  }
  async createVault(newVault) {
    if (newVault.isPrivate == 'true') {
      newVault.isPrivate = true
    } else {
      newVault.isPrivate = false
    }
    logger.log('Vaultey', newVault)
    const res = await api.post(`api/vaults`, newVault)
    logger.log('New Vault', res.data)
    AppState.vaults.unshift(res.data)
    AppState.myVaults.unshift(res.data)
  }
  sort(type) {
    let sorted = []
    if (type == 'mostRecent') {
      logger.log('mostRecent')
      sorted = AppState.vaults.sort(function (a, b) {
        return new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
      })
    }
    else if (type === 'oldest') {
      sorted = AppState.vaults.sort(function (a, b) {
        return new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime()
      })
    }
    else if (type == 'aToZ') {
      sorted = AppState.vaults.sort(function (a, b) {
        if (a.name < b.name) { return -1 }
        if (a.name > b.name) { return 1 }
        return 0
      })
    }
    else if (type == 'zToA') {
      sorted = AppState.vaults.sort(function (a, b) {
        if (a.name > b.name) { return -1 }
        if (a.name < b.name) { return 1 }
        return 0
      })
    }
    else if (type == 'private') {
      sorted = AppState.vaults.sort(function (a, b) {
        if (a.isPrivate == true) { return -1 }
        if (a.isPrivate == false) { return 1 }
        return 0
      })
    }
    AppState.vaults = sorted
  }
}

export const vaultsService = new VaultsService()