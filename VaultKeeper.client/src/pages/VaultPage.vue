<template>
  <div class="VaultPage" v-if="!loading">
    <div class="row m-0">
      <div class="col-12">
        <div class="d-flex align-items-center justify-content-between">
          <div class="info">
            <div class="title">
              {{ vault.name }}
            </div>
            <div class="description">
              {{ vault.description }}
            </div>
            <div class="stats">Keeps: {{ keeps.length }}</div>
            <div class="privacy" v-if="vault.creatorId == account.id">
              <div class="stats" v-if="vault.isPrivate">Private</div>
              <div class="stats" v-else>Public</div>
            </div>
          </div>
          <div class="delete" v-if="vault.creatorId == account.id">
            <button class="btn btn-outline-dark" @click="deleteVault()">
              Delete Vault
            </button>
          </div>
        </div>
      </div>
    </div>
    <div class="row" v-if="keeps.length > 0">
      <div
        class="col-md-4 col-sm-6 col-lg-3 m-4"
        v-for="k in keeps"
        :key="k.id"
      >
        <div class="row">
          <div class="col-12">
            <div class="container">
              <figure>
                <Keep :keep="k" />
              </figure>
            </div>
          </div>
          <div class="col-12" v-if="vault.creatorId == account.id">
            <button
              class="btn w-100 btn-danger mt-1"
              title="Remove keep from vault"
              @click="removeKeepFromVault(k.vaultKeepId, k.id)"
            >
              Remove from Vault
            </button>
          </div>
        </div>
      </div>
    </div>
    <div class="row" v-else>
      <div class="col-12">
        <div class="d-flex align-items-center justify-content-center">
          <div class="no-keeps">There Are No Keeps in this Vault Yet</div>
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
import { vaultKeepsService } from "../services/VaultKeepsService"
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
        Pop.toast('You are not authorized good sir', 'error')
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
      async removeKeepFromVault(vaultKeepId, keepId) {
        if (await Pop.confirm("are you sure you want to remove this? It won't delete the keep but it might be hard to find again")) {
          await vaultKeepsService.removeKeepFromVault(vaultKeepId, keepId)
        }
      },
      async deleteVault() {
        if (await Pop.confirm('Are you sure you would like to delete this amazing vault?')) {
          await vaultsService.deleteVault(route.params.vaultId)
          router.push({ name: 'Account', params: { accountId: AppState.account.id } })
          Pop.toast('Deleted')
        }
      },
      account: computed(() => AppState.account),
      loading: computed(() => AppState.loading),
      keeps: computed(() => AppState.keeps),
      vault: computed(() => AppState.activeVault)
    }
  },
}
</script>
<style scoped>
figure > img {
  grid-row: 1 / -1;
  grid-column: 1;
}
figure {
  margin: 0;
  display: grid;
  grid-template-rows: 1fr auto;
  margin-bottom: 10px;
  break-inside: avoid;
}
.container {
  column-count: 4;
  column-gap: 10px;
}
.title {
  font-size: 5vh;
  word-wrap: break-word;
}
.description {
  font-size: 3vh;
}
.stats {
  font-size: 3.5vh;
}
.info {
  padding-top: 20px;
  padding-left: 40px;
  word-wrap: break-word;
}
.delete {
  position: absolute;
  right: 10px;
  top: 100px;
}
.no-keeps {
  margin-top: 30vh;
  color: var(--bs-dark);
  font-weight: bold;
  transform: rotate(-30deg);
  background-color: var(--bs-primary);
  padding: 20px;
  font-size: 5vh;
}
.click-me {
  font-size: 3vh;
}
</style>