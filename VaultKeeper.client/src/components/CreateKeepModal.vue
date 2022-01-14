<template>
  <div class="modal fade bd-example-modal-sm" id="CreateKeep">
    <div class="modal-dialog modal-sm">
      <div class="modal-content bg-secondary">
        <form @submit.prevent="Create()">
          <div class="modal-header">
            <div class="modal-title">
              <div class="input">Create Keep</div>
            </div>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body">
            <div class="container-fluid">
              <div class="row">
                <div class="col-12">
                  <div class="input text-center">Keep Name</div>
                  <input
                    type="text"
                    required
                    placeholder="Keep Name ..."
                    class="form-control"
                    v-model="info.name"
                    maxlength="60"
                    minlength="1"
                  />
                  <div class="input text-center">Keep Description</div>
                  <input
                    type="text"
                    required
                    placeholder="Keep Description ..."
                    class="form-control"
                    v-model="info.description"
                    maxlength="600"
                    minlength="1"
                  />
                  <div class="input text-center">Keep Image</div>
                  <input
                    type="text"
                    required
                    placeholder="Keep Img..."
                    class="form-control"
                    v-model="info.img"
                  />
                </div>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button
              class="btn btn-success"
              title="Create Keep"
              type="submit"
              :disabled="(submitting = false)"
            >
              Submit
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>
<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import { useRouter } from "vue-router"
import { Modal } from "bootstrap"
import { profilesService } from "../services/ProfilesService"
import { keepsService } from "../services/KeepsService"
import { logger } from "../utils/Logger"
export default {
  setup() {
    const router = useRouter()
    const info = ref({})
    const submitting = ref(false)
    return {
      info,
      router,
      submitting,
      async Create() {
        submitting.value = true
        logger.log(info.value)
        await keepsService.createKeep(info.value);
        Modal.getOrCreateInstance(document.getElementById("CreateKeep")).hide();
        info.value = {}
        submitting.value = false
      },
      async RouteToCreatorPage() {
        Modal.getOrCreateInstance(document.getElementById("CreateKeep")).hide();
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
.input {
  font-size: 2vh;
}
</style>