<template>
  <div class="modal fade bd-example-modal-xl" id="KeepModal">
    <div class="modal-dialog bg-light modal-xl">
      <div class="modal-content">
        <div class="modal-body">
          <div class="container-fluid">
            <div class="row">
              <div class="col-6">
                <div class="row">
                  <div class="col-12">
                    <img
                      :src="activeKeep.img"
                      class="w-100 img elevation-2"
                      alt="Image of the Keep"
                      v-if="activeKeep.img != `{{editKeepImg}}`"
                    />
                    <img
                      src="../assets/img/KnightsIcon.jpg"
                      alt="Image broke so here is the logo"
                      class="w-100 img object-fit-cover elevation-2"
                      v-else
                    />
                  </div>
                </div>
              </div>
              <div class="col-6">
                <div class="row">
                  <div class="col-12">
                    <div class="d-flex justify-content-end">
                      <button
                        type="button"
                        class="btn-close"
                        data-bs-dismiss="modal"
                        aria-label="Close"
                      ></button>
                    </div>
                    <div class="d-flex justify-content-between">
                      <div class="icons text-shadow">
                        Views: {{ activeKeep.views }}
                      </div>
                      <div class="icons text-shadow">
                        Stored: {{ activeKeep.keeps }}
                      </div>
                      <div class="icons text-shadow">
                        Shares: {{ activeKeep.shares }}
                      </div>
                    </div>
                  </div>
                  <div class="col-12">
                    <div class="title text-center text-shadow">
                      {{ activeKeep.name }}
                    </div>
                  </div>
                  <div class="col-12 mt-4">
                    <p class="description text-shadow">
                      {{ activeKeep.description }}
                    </p>
                  </div>
                  <div class="col-12 bottom">
                    <div class="d-flex align-items-bottom">
                      <div class="dropdown">
                        <button
                          class="btn btn-success elevation-2 dropdown-toggle"
                          data-bs-toggle="dropdown"
                          title="add keep to a already created vault"
                        >
                          Add to Vault
                        </button>
                        <ul class="dropdown-menu">
                          <li
                            class="dropdown-item action"
                            v-for="v in myVaults"
                            :key="v.id"
                            @click="addToVault(v.id)"
                            title="Add keep to vault"
                          >
                            {{ v.name }}
                          </li>
                        </ul>
                      </div>
                      <button
                        class="btn btn-danger ms-2 elevation-2 text-light"
                        v-if="activeKeep.creatorId == account.id"
                        @click="deleteKeep()"
                        title="Delete this keep"
                      >
                        <i class="mdi mdi-trash-can" />
                      </button>
                      <button
                        class="
                          btn
                          selectable
                          bg-dark
                          p-1
                          elevation-2
                          rounded
                          ms-2
                        "
                        title="View the creator's page!"
                        @click="RouteToCreatorPage()"
                      >
                        <span class="mx-3 text-shadow">
                          Creator: {{ activeKeep.creator?.name }}
                        </span>
                      </button>
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
</template>
<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { useRouter } from "vue-router"
import { Modal } from "bootstrap"
import { profilesService } from "../services/ProfilesService"
import { keepsService } from "../services/KeepsService"
import { vaultKeepsService } from "../services/VaultKeepsService"
import Pop from "../utils/Pop"
export default {
  setup() {
    const router = useRouter()
    return {
      router,
      async addToVault(vaultId) {
        await vaultKeepsService.createVaultKeep(vaultId)
      },
      async deleteKeep() {
        Modal.getOrCreateInstance(document.getElementById("KeepModal")).hide();
        if (await Pop.confirm('Are You sure you want to delete this keep?')) {
          await keepsService.deleteKeep()
        } else {
          Modal.getOrCreateInstance(document.getElementById("KeepModal")).toggle();
        }
      },
      async RouteToCreatorPage() {
        Modal.getOrCreateInstance(document.getElementById("KeepModal")).hide();
        router.push({ name: 'Account', params: { accountId: AppState.activeKeep.creator?.id } })
      },
      account: computed(() => AppState.account),
      activeKeep: computed(() => AppState.activeKeep),
      myVaults: computed(() => AppState.myVaults),
    }
  },
}
</script>
<style scoped>
.bottom {
  position: absolute;
  bottom: 0;
  margin-bottom: 1.8vh;
}
.icons {
  font-size: 3vh;
}
.title {
  font-size: 4vh;
  padding-top: 3vh;
  border-bottom: 3px solid var(--bs-dark);
}
.description {
  font-size: 2vh;
  word-wrap: break-word;
}
.img {
  max-height: 800px;
  min-height: 500px;
  object-fit: cover;
}
</style>