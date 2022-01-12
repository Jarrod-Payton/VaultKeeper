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
                    <div class="description text-shadow">
                      {{ activeKeep.description }}
                    </div>
                  </div>
                  <div class="col-12 flex-grow">
                    <button class="btn btn-success elevation-2">
                      Add to Vault
                    </button>
                    <button class="btn btn-danger ms-2 elevation-2 text-light">
                      <i class="mdi mdi-trash-can" />
                    </button>
                    <button class="selectable bg-dark p-1 rounded elevation-2">
                      <img
                        :src="activekeep.creator?.picture"
                        alt="user photo"
                        height="40"
                        class="rounded"
                      />
                      <span class="mx-3 text-success lighten-30 text-shadow">{{
                        activeKeep.creator?.name
                      }}</span>
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
}
.img {
  max-height: 800px;
  min-height: 500px;
}
</style>