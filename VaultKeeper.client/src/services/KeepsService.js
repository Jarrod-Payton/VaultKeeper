import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"
class KeepsService {
  async GetAllKeeps() {
    const res = await api.get(`api/keeps`)
    logger.log(res.data)
    AppState.keeps = res.data
    keepsService.sort('mostRecent')
  }
  async setActive(keep) {
    AppState.activeKeep = keep
    const res = await api.get(`api/keeps/${keep.id}`)
    logger.log(res.data)
    AppState.activeKeep = res.data
  }
  async createKeep(newKeep) {
    const res = await api.post(`api/keeps`, newKeep)
    AppState.keeps.unshift(res.data)
  }
  async deleteKeep() {
    await api.delete(`api/keeps/${AppState.activeKeep.id}`)
    AppState.keeps = AppState.keeps.filter(k =>
      k.id != AppState.activeKeep.id
    )
    AppState.activeKeep = {}
    Pop.toast('Deleted', 'success')
  }
  sort(type) {
    let sorted = []
    if (type == 'mostRecent') {
      logger.log('mostRecent')
      sorted = AppState.keeps.sort(function (a, b) {
        return new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
      })
    }
    else if (type === 'oldest') {
      sorted = AppState.keeps.sort(function (a, b) {
        return new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime()
      })
    }
    else if (type == 'aToZ') {
      sorted = AppState.keeps.sort(function (a, b) {
        if (a.name < b.name) { return -1 }
        if (a.name > b.name) { return 1 }
        return 0
      })
    }
    else if (type == 'zToA') {
      sorted = AppState.keeps.sort(function (a, b) {
        if (a.name > b.name) { return -1 }
        if (a.name < b.name) { return 1 }
        return 0
      })
    }
    else if (type == 'private') {
      sorted = AppState.keeps.sort(function (a, b) {
        if (a.isPrivate == true) { return -1 }
        if (a.isPrivate == false) { return 1 }
        return 0
      })
    }
    AppState.keeps = sorted
  }
}
export const keepsService = new KeepsService()