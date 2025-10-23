<template>
  <div class="fee-calculator">
    <h2>Fee Calculator</h2>

    <div class="form-row">
      <label for="basePrice">Total price of vehicle</label>
      <input
        id="basePrice"
        type="number"
        v-model.number="basePrice"
        min="0"
        step="0.01"
        placeholder="Enter total price"
      />
    </div>

    <div class="form-row">
      <label for="vehicleType">Type</label>
      <select id="vehicleType" v-model="vehicleType">
        <option value="">Select Type</option>
        <option value="common">Common</option>
        <option value="luxury">Luxury</option>
      </select>
    </div>
 
    <div class="actions">
      <button @click="onCalculate" :disabled="loading || !isValid">
        {{ loading ? 'Calculating...' : 'Calculate' }}
      </button>
    </div>

    <div v-if="error" class="error">{{ error }}</div>

    <div v-if="result" class="results">
      <h3>Results</h3>
      <ul>
        <li>Buyer fee: <strong>{{ formatCurrency(result.buyerFee) }}</strong></li>
        <li>Seller's special fee: <strong>{{ formatCurrency(result.sellerFee) }}</strong></li>
        <li>Association fee: <strong>{{ formatCurrency(result.associationFee) }}</strong></li>
        <li>Storage fee: <strong>{{ formatCurrency(result.storageFee) }}</strong></li>
        <li>Total fees: <strong>{{ formatCurrency(result.totalFees) }}</strong></li>
        <li>Total : <strong>{{ formatCurrency(result.total) }}</strong></li>
      </ul>
    </div>
  </div>
</template>

<script>
import { calculateFees } from '../services/auctionApi';

export default {
  name: 'FeeCalculator',
  data() {
    return {
      basePrice: null,
      vehicleType: '', // default empty -> "Select Type"
      loading: false,
      error: null,
      result: null
    };
  },
  computed: {
    isValid() {
      return this.basePrice !== null && this.basePrice >= 0 && this.vehicleType !== '';
    }
  },
  watch: {
    // it executes every time the base price changes
    basePrice(newVal, oldVal) {
      this.triggerAutoCalculate();
    },
    // it executes every time the vehicle type changes
    vehicleType(newVal, oldVal) {
      this.triggerAutoCalculate();
    }
  },
  methods: {
    triggerAutoCalculate() {
      if (this.isValid) {
        this.onCalculate();
      } else {
        this.result = null; // clear previous result if inputs are invalid
      }
    },
    async onCalculate() {
      if (!this.isValid) {
        this.error = 'Please enter a valid total price and select a vehicle type.';
        return;
      }

      this.loading = true;
      this.error = null;
      this.result = null;

      try {
        const data = await calculateFees(this.basePrice, this.vehicleType);

        const get = (obj, ...keys) => {
          for (const k of keys) {
            if (obj && Object.prototype.hasOwnProperty.call(obj, k)) return obj[k];
          }
          return null;
        };

        this.result = {
          buyerFee: Number(get(data, 'buyerFee')) || 0,
          sellerFee: Number(get(data, 'sellerFee')) || 0,
          associationFee: Number(get(data, 'associationFee')) || 0,
          storageFee: Number(get(data, 'storageFee')) || 0,
          totalFees: Number(get(data, 'totalFees')) || 0,
        };
        this.result.total = this.basePrice + this.result.totalFees;

      } catch (err) {
        this.error = err && err.message ? err.message : 'An error occurred while calculating fees.';
      } finally {
        this.loading = false;
      }
    },
    formatCurrency(value) {
      if (value === null || value === undefined) return '-';
      // Simple USD formatting; change currency if needed
      return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(Number(value));
    }
  }
};
</script>

<style scoped>
.fee-calculator {
  max-width: 480px;
  margin: 0 auto;
  padding: 1rem;
  border: 1px solid #e5e7eb;
  border-radius: 6px;
  background: #fff;
}
.form-row {
  margin-bottom: 0.75rem;
  display: flex;
  flex-direction: column;
}
label {
  font-weight: 600;
  margin-bottom: 0.25rem;
}
input[type="number"],
select {
  padding: 0.5rem;
  border: 1px solid #cbd5e1;
  border-radius: 4px;
}
.actions {
  margin-top: 0.5rem;
}
button {
  padding: 0.5rem 1rem;
  background: #2563eb;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}
button:disabled {
  opacity: 0.6;
  cursor: not-allowed;
}
.error {
  margin-top: 0.75rem;
  color: #b91c1c;
}
.results {
  margin-top: 1rem;
  border-top: 1px dashed #e5e7eb;
  padding-top: 0.75rem;
}
.results ul {
  list-style: none;
  padding: 0;
}
.results li {
  margin-bottom: 0.5rem;
}
</style>