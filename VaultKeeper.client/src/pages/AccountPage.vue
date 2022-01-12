<template>
  <div class="Account-Page" v-if="!loading">
    <!--This is the Profile Info-->
    <div class="row">
      <div class="col-12">
        <div class="container-fluid ms-2 mt-4">
          <div class="d-flex justify-content-start align-items-center">
            <img
              :src="account.picture"
              class="elevation-2 me-3 profile-pic"
              alt="Profile Picture"
            />
            <div class="info">
              <div class="name">
                {{ account.name }}
              </div>
              <div class="stats action" @click="ScrollToKeeps()">
                Keeps: {{ keeps.length }}
              </div>
              <div class="stats action" @click="ScrollToVaults()">
                Vaults: {{ vaults.length }}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!--End of Profile Info-->
    <div class="row">
      <div class="col-12" id="vaults">
        <div class="d-flex justify-content-between align-itmes-center">
          <div class="card-titles text-shadow text-light">
            <b class="text-info">Vaults</b> created by {{ account.name }}!
          </div>
          <div>
            <button
              class="btn btn-success elevation-2 text-shadow"
              title="Create A Vault"
              @click="CreateVault()"
              v-if="user.id == account.id"
            >
              +
            </button>
            <button
              class="btn btn-secondary elevation-2 ms-2 text-shadow"
              title="Select from Vaults"
              v-if="user.id == account.id"
            >
              Select
            </button>
            <button
              class="btn btn-info elevation-2 ms-2 text-shadow dropdown-toggle"
              title="Sort the order of Vaults"
              id="sort"
              data-bs-toggle="dropdown"
            >
              Sort
            </button>
            <ul class="dropdown-menu">
              <li>
                <a
                  class="dropdown-item dropdown-item-text"
                  @click="sortVaults('oldest')"
                  >Oldest</a
                >
              </li>
              <li>
                <a
                  class="dropdown-item dropdown-item-text"
                  @click="sortVaults('mostRecent')"
                  >Most Recent</a
                >
              </li>
              <li>
                <a
                  class="dropdown-item dropdown-item-text"
                  @click="sortVaults('aToZ')"
                  >A-Z</a
                >
              </li>
              <li>
                <a
                  class="dropdown-item dropdown-item-text"
                  @click="sortVaults('zToA')"
                  >Z-A</a
                >
              </li>
            </ul>
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
    <div class="col-12" id="keeps">
      <div class="card p-3 m-4 bg-primary elevation-3">
        <div class="card-title">
          <div class="d-flex justify-content-between align-itmes-center">
            <div class="card-titles text-shadow text-light">
              <b class="text-info">Keeps</b> created by {{ account.name }}!
            </div>
            <div>
              <button
                class="btn btn-success elevation-2 text-shadow"
                title="Create A Keep"
                @click="CreateKeep()"
                v-if="user.id == account.id"
              >
                +
              </button>
              <button
                class="btn btn-secondary elevation-2 ms-2 text-shadow"
                title="Select from Vaults"
              >
                Select
              </button>
              <button
                class="
                  btn btn-info
                  elevation-2
                  ms-2
                  text-shadow
                  dropdown-toggle
                "
                title="Sort the order of Vaults"
                id="sort"
                data-bs-toggle="dropdown"
              >
                Sort
              </button>
              <ul class="dropdown-menu">
                <li>
                  <a
                    class="dropdown-item dropdown-item-text"
                    @click="sortKeeps('oldest')"
                    >Oldest</a
                  >
                </li>
                <li>
                  <a
                    class="dropdown-item dropdown-item-text"
                    @click="sortKeeps('mostRecent')"
                    >Most Recent</a
                  >
                </li>
                <li>
                  <a
                    class="dropdown-item dropdown-item-text"
                    @click="sortKeeps('aToZ')"
                    >A-Z</a
                  >
                </li>
                <li>
                  <a
                    class="dropdown-item dropdown-item-text"
                    @click="sortKeeps('zToA')"
                    >Z-A</a
                  >
                </li>
              </ul>
            </div>
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
  <Loading v-else />
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from "vue-router"
import { profilesService } from "../services/ProfilesService"
import { resetService } from "../services/ResetService"
import { logger } from "../utils/Logger"
import { vaultsService } from "../services/VaultsService"
import Pop from "../utils/Pop"
import { Modal } from "bootstrap"
import { keepsService } from "../services/KeepsService"
export default {
  name: 'Account',
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        AppState.loading = true
        await resetService.ResetEverything()
        await profilesService.getProfileDetails(route.params.accountId)
        AppState.loading = false
      } catch (error) {
        Pop.toast(error.message, 'error')
        logger.error(error.message)
      }
    })
    return {
      route,
      user: computed(() => AppState.account),
      account: computed(() => AppState.activeUser),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      loading: computed(() => AppState.loading),
      sortVaults(type) {
        vaultsService.sort(type)
      },
      sortKeeps(type) {
        keepsService.sort(type)
      },
      CreateVault() {
        Modal.getOrCreateInstance(document.getElementById("CreateVault")).toggle();
      },
      CreateKeep() {
        Modal.getOrCreateInstance(document.getElementById("CreateKeep")).toggle();
      },
      ScrollToVaults() {
        const el = document.getElementById("vaults")
        el.scrollIntoView({ behavior: "smooth" });
      },
      ScrollToKeeps() {
        const el = document.getElementById("keeps")
        el.scrollIntoView({ behavior: "smooth" });
      }
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
