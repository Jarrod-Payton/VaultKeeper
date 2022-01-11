<template>
  <div class="modal fade bd-example-modal-xl" id="KeepModal">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-body bg-primary">
          <div class="container-fluid">
            <div class="row">
              <div class="col-6">
                <div class="row">
                  <div class="col-12">
                    <img
                      :src="activeKeep.img"
                      class="w-100 img object-fit-cover elevation-2"
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
                  <div class="col-12">
                    <div
                      class="
                        creator
                        text-center text-light text-shadow
                        bg-info
                        p-1
                        mt-3
                        action
                      "
                      @click="RouteToCreatorPage()"
                    >
                      Posted By {{ activeKeep.creator?.name }}
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-6">
                <div class="row">
                  <div class="col-12">
                    <div class="title text-center text-light text-shadow">
                      {{ activeKeep.name }}
                    </div>
                  </div>
                  <div class="col-12">
                    <div class="d-flex justify-content-between">
                      <div class="icons text-light text-shadow">
                        Views: {{ activeKeep.views }}
                      </div>
                      <div class="icons text-light text-shadow">
                        Stored: {{ activeKeep.keeps }}
                      </div>
                      <div class="icons text-light text-shadow">
                        Shares: {{ activeKeep.shares }}
                      </div>
                    </div>
                  </div>
                  <div class="col-12 mt-4">
                    <div class="card bg-secondary">
                      <div class="card-body">
                        <div class="description text-shadow text-light">
                          {{ activeKeep.description }}
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="col-12 bottom">
                    <button class="btn btn-success elevation-2">
                      Add to Vault
                    </button>
                    <button class="btn btn-light ms-2 elevation-2">
                      Share
                    </button>
                    <button class="btn btn-danger ms-2 elevation-2 text-light">
                      <i class="mdi mdi-trash-can" />
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
</template>
<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { useRouter } from "vue-router"
import { Modal } from "bootstrap"
import { profilesService } from "../services/ProfilesService"
export default {
  setup() {
    const router = useRouter()
    return {
      router,
      async RouteToCreatorPage() {
        Modal.getOrCreateInstance(document.getElementById("KeepModal")).hide();
        router.push({ name: 'Account', params: { accountId: AppState.activeKeep.creator?.id } })
      },
      activeKeep: computed(() => AppState.activeKeep)
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
.creator {
  font-size: 1.5vh;
}
.icons {
  font-size: 3vh;
}
.title {
  font-size: 4vh;
}
.description {
  font-size: 2vh;
}
.img {
  max-height: 800px;
  min-height: 500px;
}
</style>