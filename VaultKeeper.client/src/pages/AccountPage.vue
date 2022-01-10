<template>
  <div class="Account-Page" v-if="!loading">
    <div class="row">
      <div class="col-3"></div>
      <div class="col-6">
        <div class="card m-3 p-3 elevation-3 bg-primary">
          <div class="card-body">
            <div class="row">
              <div class="col-3">
                <div class="d-flex justify-content-center align-items-center">
                  <img
                    :src="account.picture"
                    class="img rounded elevation-3"
                    alt="Profile Picture"
                  />
                </div>
              </div>
              <div class="col-9">
                <div class="d-flex justify-content-center align-items-center">
                  <div class="account text-shadow text-light">
                    Welcome to {{ account.name }}'s Page
                  </div>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-1"></div>
              <div class="col-10">
                <div class="d-flex justify-content-between align-items-center">
                  <div
                    class="card elevation-3 bg-secondary p-2 mt-4 selectable"
                  >
                    <div class="stats-title text-light text-center">
                      Keeps Created By {{ account.name }}
                    </div>
                    <div class="stats text-light text-center text-shadow">
                      {{ keeps.length }}
                    </div>
                  </div>
                  <div
                    class="card elevation-3 bg-secondary p-2 mt-4 selectable"
                  >
                    <div class="stats-title text-light text-center">
                      Vaults Created By {{ account.name }}
                    </div>
                    <div class="stats text-light text-center text-shadow">
                      {{ vaults.length }}
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-1"></div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-3"></div>
    </div>
    <div class="row">
      <div class="col-12">
        <div class="card p-3 m-4 mt-5 bg-primary elevation-3">
          <div class="card-title">
            <div class="card-titles text-shadow text-light">
              <b class="text-info">Vaults</b> created by {{ account.name }}!
            </div>
          </div>
          <div class="card-body">
            <div class="card bg-secondary p-2">
              <div class="card-body">
                <div class="row m-0">
                  <div
                    class="col-md-4 col-sm-3 col-lg-2"
                    v-for="v in vaults"
                    :key="v.id"
                  >
                    <Vault :vault="v" />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-12">
        <div class="card p-3 m-4 bg-primary elevation-3">
          <div class="card-title">
            <div class="card-titles text-shadow text-light">
              <b class="text-info">Keeps</b> created by {{ account.name }}!
            </div>
          </div>
          <div class="card-body">
            <div class="card bg-secondary p-2">
              <div class="card-body">
                <div class="row m-0">
                  <div
                    class="col-md-4 col-sm-6 col-lg-3"
                    v-for="k in keeps"
                    :key="k.id"
                  >
                    <Keep :keep="k" />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <Loading v-else />
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from "vue-router"
import { profilesService } from "../services/ProfilesService"
import { resetService } from "../services/ResetService"
export default {
  name: 'Account',
  setup() {
    const route = useRoute()
    onMounted(async () => {
      AppState.loading = true
      await resetService.ResetEverything()
      await profilesService.getProfileDetails(route.params.accountId)
      AppState.loading = false
    })
    return {
      route,
      account: computed(() => AppState.activeUser),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      loading: computed(() => AppState.loading)
    }
  }
}
</script>

<style scoped>
img {
  width: 100px;
}
.account {
  font-size: 3.5vh;
}
.stats {
  font-size: 2.4vh;
}
.stats-title {
  font-size: 1.5vh;
}
.card-titles {
  font-size: 3vh;
}
</style>
