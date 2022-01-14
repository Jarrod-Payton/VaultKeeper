<template>
  <div class="modal fade bd-example-modal-sm" id="CreateVault">
    <div class="modal-dialog modal-sm">
      <div class="modal-content bg-secondary">
        <form @submit.prevent="CreateVault()">
          <div class="modal-header">
            <div class="modal-title">
              <div class="input">Create Vault</div>
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
                  <div class="input text-center">Vault Name</div>
                  <input
                    type="text"
                    required
                    placeholder="Vault Name ..."
                    class="form-control"
                    maxlength="10"
                    minlength="1"
                    v-model="info.name"
                  />
                  <div class="input text-center">Vault Description</div>
                  <input
                    type="text"
                    required
                    placeholder="Keep Description ..."
                    class="form-control"
                    v-model="info.description"
                    maxlength="600"
                    minlength="1"
                  />
                  <div class="input text-center">Private Vault?</div>
                  <div
                    class="
                      d-flex
                      justify-content-between
                      align-items-center
                      pe-2
                    "
                  >
                    <div>
                      <div class="input">Yes</div>
                      <input
                        type="radio"
                        required
                        placeholder="Keep Img..."
                        class="radio"
                        value="true"
                        v-model="info.isPrivate"
                      />
                    </div>
                    <div>
                      <div class="input">No</div>
                      <input
                        type="radio"
                        required
                        placeholder="Keep Img..."
                        class="radio"
                        value="false"
                        v-model="info.isPrivate"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button
              class="btn btn-success"
              title="Create Vault"
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
import { ref } from "@vue/reactivity"
import { vaultsService } from "../services/VaultsService"
import { logger } from "../utils/Logger"
import { Modal } from "bootstrap"
export default {
  setup() {
    const info = ref({})
    const submitting = ref(false)
    return {
      info,
      submitting,
      async CreateVault() {
        submitting.value = true
        await vaultsService.createVault(info.value);
        Modal.getOrCreateInstance(document.getElementById("CreateVault")).toggle();
        info.value = {}
        submitting.value = false
      }
    }
  },
}
</script>
<style scoped>
</style>