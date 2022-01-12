<template>
  <div class="VaultPage" v-if="!loading">
    <div class="row m-0" v-if="keeps.length > 0">
      <div class="col-md-6">
        <div class="card bg-primary mt-4 m-3 elevation-2">
          <div class="card-body">
            <div class="row">
              <div class="col-12">
                <h1 class="text-light text-shadow">{{ vault.name }}</h1>
                <h1 class="text-light text-shadow">{{ vault.description }}</h1>
                <h1 class="text-light text-shadow">
                  Keeps: {{ keeps.length }}
                </h1>
              </div>
              <div class="col-12">
                <div
                  class="
                    creator
                    text-center text-light text-shadow
                    bg-info
                    p-1
                    mt-3
                    action
                    elevation-2
                  "
                  @click="RouteToCreatorPage()"
                >
                  Owned By {{ vault.creator?.name }}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-md-4 col-sm-6 col-lg-3" v-for="k in keeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
    <div class="row m-0" v-else>
      <div class="col-12">
        <div class="card bg-primary elevation-2 mt-2">
          <div class="card-body">
            <div class="row">
              <div class="col-12">
                <h1 class="text-light text-shadow">{{ vault.name }}</h1>
                <h1 class="text-light text-shadow">{{ vault.description }}</h1>
                <h1 class="text-light text-shadow">
                  Keeps: {{ keeps.length }}
                </h1>
              </div>
              <div class="col-12">
                <div
                  class="
                    creator
                    text-center text-light text-shadow
                    bg-info
                    p-1
                    mt-3
                    action
                    elevation-2
                  "
                  @click="RouteToCreatorPage()"
                >
                  Owned By {{ vault.creator?.name }}
                </div>
              </div>
              <div class="col-12">
                <div class="card mt-4 bg-danger p-3">
                  <div class="card-body">
                    <div class="row action" @click="RouteHome()">
                      <div class="col-12">
                        <div
                          class="No-Keeps text-shadow text-center text-light"
                        >
                          You haven't added any keeps to this vault!
                        </div>
                      </div>
                      <div class="col-12">
                        <div
                          class="click-me text-shadow text-light text-center"
                        >
                          (Click me to go to the home page)
                        </div>
                      </div>
                    </div>
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
import { computed, onMounted } from "@vue/runtime-core"
import { useRoute, useRouter } from "vue-router"
import { vaultsService } from "../services/VaultsService"
import { AppState } from "../AppState"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { resetService } from "../services/ResetService"
export default {
  setup() {
    const route = useRoute()
    const router = useRouter()
    onMounted(async () => {
      try {
        AppState.loading = true
        await resetService.ResetEverything()
        logger.log(route.params.vaultId)
        await vaultsService.setupVaultPage(route.params.vaultId)
        await vaultsService.GetMyVaults()
        if (AppState.activeVault.name == undefined) {
          router.push({ name: 'Home' })
          Pop.toast("this is a private album and as such you cannot view it")
        }
        AppState.loading = false
      } catch (error) {
        Pop.toast(error.message, 'error')
        logger.error(error.message)
        router.push({ name: 'Home' })
      }
    })
    return {
      RouteToCreatorPage() {
        router.push({ name: 'Account', params: { accountId: AppState.activeVault.creator?.id } })
      },
      RouteHome() {
        router.push({ name: 'Home' })

      },
      loading: computed(() => AppState.loading),
      keeps: computed(() => AppState.keeps),
      vault: computed(() => AppState.activeVault)
    }
  },
}
</script>
<style scoped>
.creator {
  font-size: 1.5vh;
}
.No-Keeps {
  font-size: 5vh;
}
.click-me {
  font-size: 3vh;
}
</style>