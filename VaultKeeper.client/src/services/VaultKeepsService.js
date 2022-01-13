import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"

class VaultKeepsService {
  async createVaultKeep(vaultId) {
    const sent = {}
    sent.vaultId = vaultId
    sent.keepId = AppState.activeKeep.id
    await api.post(`api/vaultkeeps`, sent)
    AppState.activeKeep.keeps = AppState.activeKeep.keeps + 1
    logger.log('new Keeps', AppState.activeKeep.keeps)
    Pop.toast('Added', 'success')
  }
  async removeKeepFromVault(vaultKeepId, keepId) {
    await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    AppState.keeps = AppState.keeps.filter(k => k.id !== keepId)
    Pop.toast('Removed from Vault')
  }
}

export const vaultKeepsService = new VaultKeepsService()