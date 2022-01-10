import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService {
  async getProfileDetails(profileId) {
    const Profile = await this.GetProfileById(profileId)
    const Keeps = await this.GetKeepsByProfileId(profileId)
    const Vaults = await this.GetVaultsByProfileId(profileId)
    AppState.activeUser = Profile
    AppState.keeps = Keeps
    AppState.vaults = Vaults
  }
  async GetProfileById(profileId) {
    const res = await api.get(`api/profiles/${profileId}`)
    logger.log("Profile Gotten", res.data)
    return res.data
  }
  async GetKeepsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    logger.log('Keeps', res.data)
    return res.data
  }
  async GetVaultsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    logger.log('vaults', res.data)
    return res.data
  }
}

export const profilesService = new ProfilesService()